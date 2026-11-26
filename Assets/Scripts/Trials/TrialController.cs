using System;
using System.Collections;
using System.Globalization;
using MeuPequenoReino.Telemetry;
using UnityEngine;

public sealed class TrialController : MonoBehaviour
{
    GameSession session;
    GameUI ui;
    TrialTelemetrySession telemetry;
    Action paused;
    Action<int> finished;
    bool bound, busy;
    double attemptStarted, activeElapsed;
    public bool Busy => busy;

    public void Initialize(GameSession gameSession, GameUI gameUI, Action onPause, Action<int> onFinished,
        TrialTelemetrySession trialTelemetry)
    {
        StopAllCoroutines();
        session = gameSession;
        ui = gameUI;
        telemetry = trialTelemetry;
        paused = onPause;
        finished = onFinished;
        busy = false;
        attemptStarted = activeElapsed = 0;
        if (bound)
            return;
        bound = true;
        ui.trial.servesButton.onClick.AddListener(Serve);
        ui.trial.rejectsButton.onClick.AddListener(Reject);
        ui.trial.visitButton.onClick.AddListener(Pause);
    }

    public void Present(bool resuming)
    {
        StopAllCoroutines();
        busy = true;
        session.Screen = GameScreen.Trial;
        ui.Show(GameScreen.Trial);
        var definition = session.Definition.buildings[session.CurrentBuilding];
        var trial = session.CurrentTrial;
        var view = ui.trial;
        view.buildingName.text = definition.displayName;
        view.ShowProgress(session.Pieces(session.CurrentBuilding), definition.requiredPieces);
        view.candidatePanel.color = new Color32(241, 230, 200, 255);
        view.feedback.text = "Observe com calma. Não há limite de tempo.";
        view.reference.Present(trial.model, false, Quaternion.identity);
        view.candidate.Present(trial.model, !trial.structuralMatch, Quaternion.AngleAxis(trial.rotation, trial.rotationAxis));
        view.reference.gameObject.SetActive(false);
        view.candidate.gameObject.SetActive(false);
        view.SetInteraction(false);
        view.portrait.Present(100 + trial.model.modelId, !resuming);
        StartCoroutine(Reveal(resuming));
    }

    IEnumerator Reveal(bool resuming)
    {
        while (!ui.trial.portrait.HasArrived)
            yield return null;
        ui.trial.reference.gameObject.SetActive(true);
        ui.trial.candidate.gameObject.SetActive(true);
        if (!resuming)
        {
            activeElapsed = 0;
            var trial = session.CurrentTrial;
            telemetry.BeginTrial(session.Definition.buildings[session.CurrentBuilding].id, trial.model.modelId,
                trial.structuralMatch, trial.rotation, AxisName(trial.rotationAxis), Time.realtimeSinceStartupAsDouble);
        }
        attemptStarted = Time.realtimeSinceStartupAsDouble;
        busy = false;
        ui.trial.SetInteraction(true);
    }

    void Serve()
    {
        Answer(true);
    }

    void Reject()
    {
        Answer(false);
    }

    public void Answer(bool serves)
    {
        if (session.Screen != GameScreen.Trial || busy || session.Paused)
            return;
        busy = true;
        ui.trial.SetInteraction(false);
        var definition = session.Definition.buildings[session.CurrentBuilding];
        var trial = session.CurrentTrial;
        bool correct = session.IsCorrect(serves);
        double rt = activeElapsed + Time.realtimeSinceStartupAsDouble - attemptStarted;
        Debug.Log($"building={definition.id} candidate_index={session.CandidateIndex} model_id={trial.model.modelId} " + $"stimulus=3D rotation={trial.rotation} axis={trial.rotationAxis} candidate_condition={(trial.structuralMatch ? "MATCH" : "MIRRORED")} " + $"structural_match={trial.structuralMatch} response={(serves ? "SERVE" : "NAO_SERVE")} correct={correct} accepted_pieces={session.Pieces(session.CurrentBuilding)} reaction_time={rt.ToString("F3", CultureInfo.InvariantCulture)}");
        StartCoroutine(Resolve(correct, trial.structuralMatch));
        telemetry.RecordAttempt(serves, correct, rt);
    }

    IEnumerator Resolve(bool correct, bool structuralMatch)
    {
        if (!correct)
        {
            ui.trial.feedback.text = "Vamos observar mais uma vez.";
            yield return new WaitForSecondsRealtime(1.2f);
            ui.trial.feedback.text = "Observe com calma. Não há limite de tempo.";
            activeElapsed = 0;
            attemptStarted = Time.realtimeSinceStartupAsDouble;
            busy = false;
            ui.trial.SetInteraction(true);
            yield break;
        }

        ui.trial.feedback.text = structuralMatch ? "Isso! É a mesma peça, em outra posição." : "Isso! A peça espelhada é diferente.";
        ui.trial.candidatePanel.color = structuralMatch ? new Color32(217, 232, 200, 255) : new Color32(231, 220, 194, 255);
        int building = session.CurrentBuilding;
        var result = session.ApplyCorrectAnswer();
        yield return new WaitForSecondsRealtime(1.2f);
        if (result == AnswerResult.BuildingCompleted)
        {
            busy = false;
            activeElapsed = 0;
            finished?.Invoke(building);
            yield break;
        }

        activeElapsed = 0;
        Present(false);
    }

    public void Pause()
    {
        if (busy || session.Screen != GameScreen.Trial || session.Paused)
            return;
        double now = Time.realtimeSinceStartupAsDouble;
        activeElapsed += now - attemptStarted;
        Debug.Log($"building={session.Definition.buildings[session.CurrentBuilding].id} candidate_index={session.CandidateIndex} trial_status=PAUSED");
        if (session.Pause())
        {
            telemetry.PauseTrial(now);
            paused?.Invoke();
        }
    }

    public void Resume()
    {
        if (busy || !session.Resume())
            return;
        telemetry.ResumeTrial(Time.realtimeSinceStartupAsDouble);
        Debug.Log($"building={session.Definition.buildings[session.CurrentBuilding].id} candidate_index={session.CandidateIndex} trial_status=RESUMED");
        Present(true);
    }

    static string AxisName(Vector3 axis)
    {
        if (axis.Equals(Vector3.right))
            return "X";
        if (axis.Equals(Vector3.up))
            return "Y";
        if (axis.Equals(Vector3.forward))
            return "Z";
        Debug.LogWarning($"[Telemetry] Eixo de rotação fora do esquema 1.0: {axis}.");
        return string.Empty;
    }
}
