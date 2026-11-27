using System;
using System.Globalization;
using System.IO;
using System.Linq;
using MeuPequenoReino.Core;
using MeuPequenoReino.Telemetry;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class TrialTelemetryChecks
{
    const string Key = "MPRTelemetryCheck";
    const string ReportDirectory = "Library/TelemetryValidation";
    const string PlayReport = ReportDirectory + "/play.txt";
    static int stage;
    static double entered, firstSegment, pauseDuration, nextTick;
    static string sessionFile, trialId;
    static long firstResponse;

    static TrialTelemetryChecks()
    {
        EditorApplication.update += Tick;
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    public static void RunStorage()
    {
        Directory.CreateDirectory(ReportDirectory);
        string report = ReportDirectory + "/storage.txt";
        File.WriteAllText(report, $"Application.persistentDataPath = {Application.persistentDataPath}\n");
        string root = Path.Combine(Path.GetTempPath(), "MPR-Telemetry-" + Guid.NewGuid().ToString("N"));
        try
        {
            var session = new TrialTelemetrySession("synthetic-test", root);
            string file = SessionFiles(root).Single();
            var initial = Read(file);
            Check(initial.schema_version == "1.0" && initial.game_version == "synthetic-test" &&
                Guid.TryParseExact(initial.session_id, "N", out _) &&
                DateTime.TryParseExact(initial.started_at_utc, "O", CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind, out var started) && started.Kind == DateTimeKind.Utc &&
                initial.trials.Count == 0 && Path.GetFileName(file) == $"session_{initial.session_id}.json",
                "Cria JSON de sessão com metadados, GUID e data UTC.", report);

            session.BeginTrial("synthetic-building", 7, false, 135, "Z", 1);
            session.RecordAttempt(true, false, 4.350);
            var first = Read(file).trials.Single();
            Check(first.status == "in_progress" && first.attempts.Count == 1 && first.attempts[0].response_time_ms == 4350,
                "Persiste o primeiro erro enquanto o trial ainda está ativo.", report);
            session.PauseTrial(10);
            session.ResumeTrial(11.25);
            session.PauseTrial(14);
            session.ResumeTrial(16);
            Check(Read(file).trials.Single().paused_duration_ms == 3250,
                "Acumula duas pausas explícitas.", report);

            string previous = File.ReadAllText(file);
            string directory = Path.GetDirectoryName(file);
            string held = directory + "-held";
            int logged = 0;
            void OnLog(string message, string stack, LogType type)
            {
                if (type == LogType.Error && message.StartsWith("[Telemetry] Falha ao salvar", StringComparison.Ordinal))
                    logged++;
            }
            Directory.Move(directory, held);
            File.WriteAllText(directory, "synthetic directory conflict");
            Application.logMessageReceived += OnLog;
            try
            {
                session.RecordAttempt(true, false, 1.125);
            }
            finally
            {
                Application.logMessageReceived -= OnLog;
                File.Delete(directory);
                Directory.Move(held, directory);
            }
            Check(logged == 1 && File.ReadAllText(file) == previous,
                "Falha de I/O chega ao Console e o último JSON permanece íntegro.", report);

            session.RecordAttempt(false, true, 2.890);
            var completed = Read(file).trials.Single();
            Check(completed.status == "completed" && completed.trial_id == first.trial_id &&
                completed.paused_duration_ms == 3250 && completed.attempts.Count == 3 &&
                completed.attempts.Select(a => a.attempt_index).SequenceEqual(new[] { 1, 2, 3 }) &&
                completed.attempts.Select(a => a.response_time_ms).SequenceEqual(new long[] { 4350, 1125, 2890 }) &&
                completed.attempts.Select(a => a.is_correct).SequenceEqual(new[] { false, false, true }) &&
                completed.attempts.Select(a => a.player_answer).SequenceEqual(new[] { "SERVE", "SERVE", "NAO_SERVE" }),
                "A próxima gravação recupera a tentativa em memória, preserva a ordem e conclui o trial.", report);

            session.BeginTrial("synthetic-building", 8, true, 180, "Y", 20);
            previous = File.ReadAllText(file);
            var newer = new TrialTelemetrySession("synthetic-test", root);
            string newFile = SessionFiles(root).Single(path => path != file);
            Check(Read(newFile).session_id != initial.session_id && File.ReadAllText(file) == previous &&
                Read(file).trials.Last().status == "in_progress",
                "Outra execução deixa o snapshot parcial anterior intacto.", report);
            newer.End(21);
            session.End(22);
            Check(Read(file).trials.Last().status == "interrupted" && Read(file).trials[0].status == "completed" &&
                Directory.GetFiles(directory, "*.tmp").Length == 0,
                "Encerramento interrompe apenas o trial ativo e preserva o concluído.", report);
        }
        finally
        {
            if (Directory.Exists(root))
                Directory.Delete(root, true);
        }
    }

    public static void RunPlay()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            throw new InvalidOperationException("Execute em Edit Mode.");
        for (int i = 0; i < SceneManager.sceneCount; i++)
            if (SceneManager.GetSceneAt(i).isDirty)
                throw new InvalidOperationException("Salve as cenas modificadas antes de executar a verificação.");

        Directory.CreateDirectory(ReportDirectory);
        string root = Application.persistentDataPath;
        File.WriteAllText(PlayReport, $"Application.persistentDataPath = {root}\n");
        SessionState.SetString(Key + "Files", string.Join("|", SessionFiles(root)));
        SessionState.SetFloat(Key + "Started", (float)EditorApplication.timeSinceStartup);
        SessionState.SetBool(Key + "Background", PlayerSettings.runInBackground);
        SessionState.SetBool(Key + "Active", true);
        SessionState.SetString(Key + "ClosingFile", "");
        stage = 0;
        nextTick = 0;
        EditorApplication.isPaused = false;
        EditorSceneManager.OpenScene("Assets/Scenes/Kingdom.unity");
        EditorApplication.isPlaying = true;
    }

    static void Tick()
    {
        if (!SessionState.GetBool(Key + "Active", false) || !EditorApplication.isPlaying || stage < 0)
            return;
        EditorApplication.QueuePlayerLoopUpdate();
        if (EditorApplication.isPaused)
            EditorApplication.isPaused = false;
        double editorTime = EditorApplication.timeSinceStartup;
        if (editorTime < nextTick)
            return;
        nextTick = editorTime + .025;
        Application.runInBackground = true;
        try
        {
            var app = UnityEngine.Object.FindFirstObjectByType<GameApplication>();
            if (editorTime - SessionState.GetFloat(Key + "Started", 0) > 90)
                throw new TimeoutException($"Tempo excedido na etapa {stage}. Tela: {app?.Session?.Screen}, " +
                    $"trial ocupado: {app?.trials.Busy}, retrato pronto: {app?.ui.trial.portrait.HasArrived}, quadro: {Time.frameCount}.");
            if (app != null && app.Village != null)
                Drive(app, Time.realtimeSinceStartupAsDouble);
        }
        catch (Exception error)
        {
            File.AppendAllText(PlayReport, $"FAIL {error}\n");
            Stop();
        }
    }

    static void Drive(GameApplication app, double now)
    {
        var session = app.Session;
        var ui = app.ui;
        var ready = session.Screen == GameScreen.Trial && !app.trials.Busy;
        switch (stage)
        {
            case 0:
                var previous = SessionState.GetString(Key + "Files", "").Split('|');
                sessionFile = SessionFiles(Application.persistentDataPath).Except(previous).Single();
                var initial = Read(sessionFile);
                Check(initial.game_version == Application.version && initial.trials.Count == 0,
                    "Menu cria sessão com a versão configurada.");
                ui.menu.startButton.onClick.Invoke();
                Check(Read(sessionFile).trials.Count == 0, "Tutorial fica fora dos trials.");
                Advance(now);
                break;
            case 1 when session.Screen == GameScreen.Tutorial:
                AdvanceTutorial(ui);
                break;
            case 1 when ready:
                var trial = Read(sessionFile).trials.Single();
                CheckStimulus(app, trial, 1);
                Check(trial.status == "in_progress" && trial.attempts.Count == 0 &&
                    ui.trial.reference.gameObject.activeInHierarchy && ui.trial.servesButton.interactable,
                    "Apresentação abre um registro sem tentativas.");
                trialId = trial.trial_id;
                Advance(now);
                break;
            case 2 when now - entered >= .35:
                bool expected = app.definition.GetTrial(session).structuralMatch;
                Respond(app, !expected);
                Respond(app, expected);
                var wrong = Read(sessionFile).trials.Single().attempts.Single();
                Check(wrong.attempt_index == 1 && !wrong.is_correct &&
                    wrong.player_answer == (expected ? "NAO_SERVE" : "SERVE") &&
                    NearActiveTime(wrong.response_time_ms, now - entered) && session.CandidateIndex == 0 &&
                    ui.trial.feedback.text == "Vamos observar mais uma vez.",
                    "Registra só o erro aceito, com tempo ativo e feedback preservado.");
                firstResponse = wrong.response_time_ms;
                Advance(now);
                break;
            case 3 when ready:
                Advance(now);
                break;
            case 4 when now - entered >= .30:
                firstSegment = now - entered;
                ui.trial.visitButton.onClick.Invoke();
                Check(session.Paused && ui.village.gameObject.activeInHierarchy,
                    "Visita à vila pausa a tentativa.");
                Advance(now);
                break;
            case 5 when now - entered >= 1.35:
                pauseDuration = now - entered;
                ui.village.actionButton.onClick.Invoke();
                var resumed = Read(sessionFile).trials.Single();
                Check(!session.Paused && resumed.trial_id == trialId &&
                    resumed.attempts.Single().response_time_ms == firstResponse &&
                    Math.Abs(resumed.paused_duration_ms - pauseDuration * 1000) < 150,
                    "Retomada preserva ID, erro anterior e tempo de pausa separado.");
                Advance(now);
                break;
            case 6 when ready:
                Advance(now);
                break;
            case 7 when now - entered >= .30:
                bool answer = app.definition.GetTrial(session).structuralMatch;
                Respond(app, answer);
                var completed = Read(sessionFile).trials.Single();
                Check(completed.trial_id == trialId && completed.status == "completed" && completed.attempts.Count == 2 &&
                    completed.attempts[0].response_time_ms == firstResponse && completed.attempts[1].attempt_index == 2 &&
                    completed.attempts[1].is_correct && completed.attempts[1].player_answer == (answer ? "SERVE" : "NAO_SERVE") &&
                    NearActiveTime(completed.attempts[1].response_time_ms, firstSegment + now - entered) &&
                    completed.attempts[1].response_time_ms < pauseDuration * 1000,
                    "Acerto conclui com segunda tentativa e soma os trechos ativos sem a pausa.");
                Advance(now);
                break;
            case 8 when ready:
                var next = Read(sessionFile).trials.Last();
                CheckStimulus(app, next, 2);
                Check(next.trial_id != trialId && next.status == "in_progress", "Próxima apresentação recebe outro ID.");
                ui.trial.visitButton.onClick.Invoke();
                Advance(now);
                break;
            case 9 when now - entered >= .30:
                var files = SessionFiles(Application.persistentDataPath);
                app.Restart();
                string oldFile = sessionFile;
                string saved = File.ReadAllText(oldFile);
                sessionFile = SessionFiles(Application.persistentDataPath).Except(files).Single();
                var ended = Read(oldFile);
                Check(ended.trials[0].status == "completed" && ended.trials[1].status == "interrupted" &&
                    ended.trials[1].paused_duration_ms >= 250 && ended.trials[1].attempts.Count == 0 &&
                    Read(sessionFile).session_id != ended.session_id && Read(sessionFile).trials.Count == 0 &&
                    app.Session.Screen == GameScreen.Menu, "Reinício interrompe o trial pausado e cria outro arquivo.");
                ui.menu.startButton.onClick.Invoke();
                Check(File.ReadAllText(oldFile) == saved, "A nova sessão não reescreve o arquivo anterior.");
                File.AppendAllText(PlayReport, $"Controlled session = {oldFile}\n");
                Advance(now);
                break;
            case 10 when session.Screen == GameScreen.Tutorial:
                AdvanceTutorial(ui);
                break;
            case 10 when ready:
                SessionState.SetString(Key + "ClosingFile", sessionFile);
                Stop();
                break;
        }
    }

    static void AdvanceTutorial(GameUI ui)
    {
        if (ui.tutorialFirst.gameObject.activeInHierarchy)
            ui.tutorialFirst.nextButton.onClick.Invoke();
        else if (ui.tutorialSecond.gameObject.activeInHierarchy)
            ui.tutorialSecond.nextButton.onClick.Invoke();
    }

    static void Respond(GameApplication app, bool serves)
    {
        var view = app.ui.trial;
        (serves ? view.servesButton : view.rejectsButton).onClick.Invoke();
    }

    static void CheckStimulus(GameApplication app, TelemetryTrialRecord record, int index)
    {
        var session = app.Session;
        var stimulus = app.definition.GetTrial(session);
        string axis = stimulus.rotationAxis == Vector3.right ? "X" : stimulus.rotationAxis == Vector3.up ? "Y" : "Z";
        Check(Guid.TryParseExact(record.trial_id, "N", out _) && record.trial_index == index &&
            record.building_id == app.definition.buildings[session.CurrentBuilding].id &&
            record.model_id == stimulus.model.modelId && record.rotation_angle_degrees == stimulus.rotation &&
            record.rotation_axis == axis && record.condition == (stimulus.structuralMatch ? "rotated" : "mirrored_rotated") &&
            record.expected_answer == (session.IsCorrect(true) ? "SERVE" : "NAO_SERVE"),
            $"Trial {index} corresponde à configuração real.");
    }

    static bool NearActiveTime(long recorded, double seconds) =>
        recorded >= seconds * 1000 - 25 && recorded <= seconds * 1000 + 300;

    static void Advance(double now)
    {
        stage++;
        entered = now;
    }

    static void Stop()
    {
        stage = -1;
        EditorApplication.isPlaying = false;
    }

    static void OnPlayModeChanged(PlayModeStateChange mode)
    {
        if (mode != PlayModeStateChange.EnteredEditMode || !SessionState.GetBool(Key + "Active", false))
            return;
        SessionState.SetBool(Key + "Active", false);
        PlayerSettings.runInBackground = SessionState.GetBool(Key + "Background", false);
        bool success = false;
        try
        {
            string file = SessionState.GetString(Key + "ClosingFile", "");
            Check(!string.IsNullOrEmpty(file) && Read(file).trials.Single().status == "interrupted",
                "Ao sair do Play Mode, o trial ativo termina como interrompido.");
            success = true;
        }
        catch (Exception error)
        {
            File.AppendAllText(PlayReport, $"FAIL {error}\n");
        }
        if (Application.isBatchMode)
            EditorApplication.Exit(success ? 0 : 1);
    }

    static string[] SessionFiles(string root)
    {
        string directory = Path.Combine(root, "Telemetry");
        return Directory.Exists(directory) ? Directory.GetFiles(directory, "session_*.json") : Array.Empty<string>();
    }

    static TelemetrySessionRecord Read(string file) => JsonUtility.FromJson<TelemetrySessionRecord>(File.ReadAllText(file));

    static void Check(bool condition, string text, string report = PlayReport)
    {
        if (!condition)
            throw new InvalidOperationException(text);
        File.AppendAllText(report, $"PASS {text}\n");
    }
}
