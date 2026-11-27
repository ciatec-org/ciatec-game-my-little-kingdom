using System;
using System.IO;
using System.Linq;
using MeuPequenoReino.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>Functional regression driver. Invokes configured UI events in real Play Mode.</summary>
[InitializeOnLoad]
public static class PlayModeFlowChecks
{
    const string Key = "MPRSmokeStage";
    static double nextTick;
    static bool failing;
    static PlayModeFlowChecks()
    {
        Install();
    }

    [InitializeOnEnterPlayMode]
    static void EnterPlayMode(EnterPlayModeOptions options)
    {
        Install();
    }

    static void Install()
    {
        EditorApplication.update -= Tick;
        EditorApplication.update += Tick;
        EditorApplication.playModeStateChanged -= ModeChanged;
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    [MenuItem("Meu Pequeno Reino/Verificar fluxo em Play Mode")]
    public static void Run()
    {
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            if (UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).isDirty)
                throw new InvalidOperationException("Salve as cenas modificadas antes de executar esta verificação.");
        if (EditorApplication.isPlaying)
            throw new InvalidOperationException("Já está em Play Mode.");
        Directory.CreateDirectory("Migration");
        File.WriteAllText("Migration/play-validation.txt", "RUNNING\n");
        SessionState.SetInt(Key, 1);
        SessionState.SetFloat("MPRSmokeStart", (float)EditorApplication.timeSinceStartup);
        SessionState.SetBool("MPRSmokePausedObservatory", false);
        SessionState.SetBool("MPRSmokeVisitedObservatory", false);
        SessionState.SetBool("MPRSmokeRendered", false);
        SessionState.SetBool("MPRSmokeBackground", PlayerSettings.runInBackground);
        EditorApplication.isPaused = false;
        Application.runInBackground = true;
        EditorSceneManager.OpenScene("Assets/Scenes/Kingdom.unity");
        EditorApplication.isPlaying = true;
    }

    static void Check(bool condition, string message)
    {
        if (!condition)
            throw new InvalidOperationException(message);
        File.AppendAllText("Migration/play-validation.txt", "PASS " + message + "\n");
    }

    static void Stage(int stage)
    {
        SessionState.SetInt(Key, stage);
        SessionState.SetFloat("MPRSmokeStageStart", (float)EditorApplication.timeSinceStartup);
    }

    static void Tick()
    {
        int stage = SessionState.GetInt(Key, 0);
        if (stage == 0 || stage == 100 || failing)
            return;
        EditorApplication.QueuePlayerLoopUpdate();
        if (EditorApplication.isPaused)
            EditorApplication.isPaused = false;
        if (!EditorApplication.isPlaying)
            return;
        if (EditorApplication.timeSinceStartup < nextTick)
            return;
        nextTick = EditorApplication.timeSinceStartup + .1;
        try
        {
            File.WriteAllText("Migration/play-heartbeat.txt", $"stage={stage} playing={EditorApplication.isPlaying} time={EditorApplication.timeSinceStartup}");
            if (EditorApplication.timeSinceStartup - SessionState.GetFloat("MPRSmokeStart", 0) > 600)
                throw new TimeoutException("Play Mode smoke test timed out.");
            Application.runInBackground = true;
            var app = UnityEngine.Object.FindFirstObjectByType<GameApplication>();
            if (app == null || app.Session == null || app.Village == null)
                return;
            var session = app.Session;
            var definition = app.definition;
            var ui = app.ui;
            double since = EditorApplication.timeSinceStartup - SessionState.GetFloat("MPRSmokeStageStart", 0);
            if (stage == 1)
            {
                Check(session.Screen == GameScreen.Menu && ui.menu.gameObject.activeInHierarchy, "Menu and application initialized");
                Check(UnityEngine.Object.FindObjectsByType<GameApplication>(FindObjectsSortMode.None).Length == 1, "Single game application");
                CaptureUI(app, "Migration/menu.png");
                ui.menu.startButton.onClick.Invoke();
                Stage(2);
            }
            else if (stage == 2)
            {
                if (since < 1.4)
                    return;
                Check(ui.tutorialFirst.gameObject.activeInHierarchy, "Rotation tutorial opens");
                Check(Quaternion.Angle(ui.tutorialFirst.candidate.meshFilter.transform.localRotation, Quaternion.Euler(22, -34, 0) * Quaternion.AngleAxis(135, Vector3.up)) > .1f, "Tutorial rotation animates mesh");
                CaptureUI(app, "Migration/tutorial.png");
                ui.tutorialFirst.nextButton.onClick.Invoke();
                Stage(3);
            }
            else if (stage == 3)
            {
                Check(ui.tutorialSecond.gameObject.activeInHierarchy, "Reflection tutorial opens");
                ui.tutorialSecond.backButton.onClick.Invoke();
                Check(ui.tutorialFirst.gameObject.activeInHierarchy, "Tutorial back button works");
                ui.tutorialFirst.nextButton.onClick.Invoke();
                ui.tutorialSecond.nextButton.onClick.Invoke();
                Stage(4);
            }
            else if (stage == 4)
            {
                if (app.trials.Busy || session.Screen != GameScreen.Trial)
                    return;
                Check(session.CurrentBuilding == definition.IndexOf(definition.firstBuilding) && session.CandidateIndex == 0, "First building and first trial preserved");
                if (definition.GetTrial(session).structuralMatch)
                    ui.trial.rejectsButton.onClick.Invoke();
                else
                    ui.trial.servesButton.onClick.Invoke();
                Stage(5);
            }
            else if (stage == 5)
            {
                if (app.trials.Busy)
                    return;
                Check(session.CandidateIndex == 0 && session.Pieces(session.CurrentBuilding) == 0, "Incorrect response repeats stimulus without reward");
                ui.trial.visitButton.onClick.Invoke();
                Check(session.Paused && ui.village.gameObject.activeInHierarchy, "Trial pauses into village");
                Check(!app.Village.Find(definition.buildings.First(b => b.prerequisite != null)).button.interactable, "Mill interaction locked before bridge");
                ui.village.actionButton.onClick.Invoke();
                Stage(6);
            }
            else if (stage == 6)
            {
                if (app.trials.Busy)
                    return;
                Check(!session.Paused && session.CandidateIndex == 0, "Resume preserves stimulus");
                Stage(7);
            }
            else if (stage == 7)
            {
                if (session.Screen == GameScreen.Trial)
                {
                    if (app.trials.Busy)
                        return;
                    var trial = definition.GetTrial(session);
                    Check(ui.trial.reference.meshFilter.sharedMesh == trial.model.referenceMesh && ui.trial.candidate.meshFilter.sharedMesh == (trial.structuralMatch ? trial.model.referenceMesh : trial.model.mirroredMesh), "Displayed meshes match stimulus " + trial.model.modelId);
                    if (!SessionState.GetBool("MPRSmokeRendered", false))
                    {
                        CaptureModel(ui.trial.reference, "Migration/model-reference.png");
                        CaptureModel(ui.trial.candidate, "Migration/model-candidate.png");
                        CaptureUI(app, "Migration/trial.png");
                        SessionState.SetBool("MPRSmokeRendered", true);
                    }

                    if (session.CompletedBuildings > 0 && session.CandidateIndex == 0 && !SessionState.GetBool("MPRSmokePausedObservatory", false))
                    {
                        int building = session.CurrentBuilding;
                        ui.trial.visitButton.onClick.Invoke();
                        var observatory = app.Village.buildings.First(b => b.definition != null && b.definition.opensObservatory);
                        observatory.button.onClick.Invoke();
                        Check(session.Screen == GameScreen.Observatory && session.Paused, "Observatory accessible during paused trial");
                        ui.observatory.backButton.onClick.Invoke();
                        Check(session.Paused && session.CurrentBuilding == building, "Return from observatory preserves paused building");
                        ui.village.actionButton.onClick.Invoke();
                        SessionState.SetBool("MPRSmokePausedObservatory", true);
                        return;
                    }

                    if (trial.structuralMatch)
                        ui.trial.servesButton.onClick.Invoke();
                    else
                        ui.trial.rejectsButton.onClick.Invoke();
                }
                else if (session.Screen == GameScreen.Village || session.Screen == GameScreen.FinalVillage)
                {
                    if (app.Village.Busy)
                        return;
                    if (session.Complete)
                    {
                        Check(session.CompletedBuildings == 5 && ui.village.actionLabel.text == "REINICIAR", "All five buildings complete and restart available");
                        Check(app.Village.animals.Length > 0 && app.Village.villagers.Length == 14, "Village contains villagers and animals");
                        CaptureWorld(app.Village.cameraDrag.mapCamera, "Migration/village-complete.png");
                        var camera = app.Village.cameraDrag;
                        camera.Focus(new Vector2(10000, 10000));
                        Check(camera.transform.position.x < 30 && camera.transform.position.y < 30, "Camera drag bounds clamp navigation");
                        ui.village.actionButton.onClick.Invoke();
                        Check(!app.Session.Complete && app.Session.CurrentBuilding == -1 && ui.menu.gameObject.activeInHierarchy, "Restart clears session and returns to menu");
                        ui.menu.startButton.onClick.Invoke();
                        ui.tutorialFirst.nextButton.onClick.Invoke();
                        ui.tutorialSecond.nextButton.onClick.Invoke();
                        Stage(8);
                        return;
                    }

                    if (!SessionState.GetBool("MPRSmokeVisitedObservatory", false))
                    {
                        var observatory = app.Village.buildings.First(b => b.definition != null && b.definition.opensObservatory);
                        observatory.button.onClick.Invoke();
                        Check(ui.observatory.gameObject.activeInHierarchy, "Completed observatory opens");
                        CaptureUI(app, "Migration/observatory.png");
                        var target = ui.observatory.GetComponentsInChildren<CelestialTarget>().First();
                        target.OnPointerEnter(new PointerEventData(EventSystem.current));
                        Check(ui.observatory.objectName.text == target.objectName && ui.observatory.objectType.text == target.objectType, "Observatory information responds to pointer");
                        target.OnPointerExit(null);
                        ui.observatory.backButton.onClick.Invoke();
                        SessionState.SetBool("MPRSmokeVisitedObservatory", true);
                    }

                    int[] order =
                    {
                        0,
                        3,
                        4,
                        1,
                        2
                    };
                    int next = order.First(i => !app.Session.IsBuilt(i));
                    var building = app.Village.Find(definition.buildings[next]);
                    Check(building.button.interactable, "Next building is unlocked: " + building.definition.id);
                    building.button.onClick.Invoke();
                    ui.village.actionButton.onClick.Invoke();
                }
            }
            else if (stage == 8)
            {
                if (app.trials.Busy || session.Screen != GameScreen.Trial)
                    return;
                Check(session.CandidateIndex == 0 && session.Pieces(session.CurrentBuilding) == 0, "Second run starts cleanly");
                File.AppendAllText("Migration/play-validation.txt", "COMPLETE — real Play Mode flow passed\n");
                Stage(100);
                EditorApplication.isPlaying = false;
            }
        }
        catch (Exception exception)
        {
            failing = true;
            File.AppendAllText("Migration/play-validation.txt", "FAIL " + exception + "\n");
            Stage(100);
            EditorApplication.isPlaying = false;

        }
    }

    static void CaptureModel(ModelPresentation presentation, string path)
    {
        var target = presentation.surface.texture as RenderTexture;
        Check(target != null && target.IsCreated(), "Model RenderTexture exists");
        var previous = RenderTexture.active;
        RenderTexture.active = target;
        var image = new Texture2D(target.width, target.height, TextureFormat.RGBA32, false);
        try
        {
            image.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
            image.Apply();
            Check(image.GetPixels32().Count(p => p.a > 0) > 100, "Model is rendered with visible pixels");
            Check(image.GetPixels32().Count(p => p.a == 0) > 100, "Model viewport preserves transparent background");
            File.WriteAllBytes(path, image.EncodeToPNG());
        }
        finally
        {
            RenderTexture.active = previous;
            UnityEngine.Object.DestroyImmediate(image);
        }
    }

    static void CaptureUI(GameApplication app, string path)
    {
        var canvas = app.ui.GetComponent<Canvas>();
        var oldMode = canvas.renderMode;
        var oldCamera = canvas.worldCamera;
        float oldDistance = canvas.planeDistance;
        Vector3 oldScale = app.ui.frame.localScale;
        var go = new GameObject("UI capture camera", typeof(Camera));
        var camera = go.GetComponent<Camera>();
        camera.enabled = false;
        camera.orthographic = true;
        camera.orthographicSize = 450;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color32(25, 31, 37, 255);
        camera.cullingMask = 1 << 5;
        camera.nearClipPlane = .1f;
        camera.farClipPlane = 100;
        go.transform.position = new Vector3(0, 0, -10);
        var target = new RenderTexture(1600, 900, 24, RenderTextureFormat.ARGB32);
        target.Create();
        camera.targetTexture = target;
        var previous = RenderTexture.active;
        var image = new Texture2D(1600, 900, TextureFormat.RGBA32, false);
        try
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = camera;
            canvas.planeDistance = 1;
            Canvas.ForceUpdateCanvases();
            app.ui.frame.localScale = Vector3.one * Mathf.Min(app.ui.canvasRect.rect.width / 1600f, app.ui.canvasRect.rect.height / 900f);
            Canvas.ForceUpdateCanvases();
            RenderPipeline.SubmitRenderRequest(camera, new UniversalRenderPipeline.SingleCameraRequest { destination = target });
            RenderTexture.active = target;
            image.ReadPixels(new Rect(0, 0, 1600, 900), 0, 0);
            image.Apply();
            File.WriteAllBytes(path, image.EncodeToPNG());
        }
        finally
        {
            canvas.renderMode = oldMode;
            canvas.worldCamera = oldCamera;
            canvas.planeDistance = oldDistance;
            app.ui.frame.localScale = oldScale;
            Canvas.ForceUpdateCanvases();
            RenderTexture.active = previous;
            camera.targetTexture = null;
            UnityEngine.Object.DestroyImmediate(image);
            target.Release();
            UnityEngine.Object.DestroyImmediate(target);
            UnityEngine.Object.DestroyImmediate(go);
        }
    }

    static void CaptureWorld(Camera camera, string path)
    {
        Canvas.ForceUpdateCanvases();
        var target = new RenderTexture(1600, 900, 24, RenderTextureFormat.ARGB32);
        target.Create();
        var previous = RenderTexture.active;
        var image = new Texture2D(1600, 900, TextureFormat.RGBA32, false);
        try
        {
            RenderPipeline.SubmitRenderRequest(camera, new UniversalRenderPipeline.SingleCameraRequest { destination = target });
            RenderTexture.active = target;
            image.ReadPixels(new Rect(0, 0, 1600, 900), 0, 0);
            image.Apply();
            File.WriteAllBytes(path, image.EncodeToPNG());
        }
        finally
        {
            RenderTexture.active = previous;
            UnityEngine.Object.DestroyImmediate(image);
            target.Release();
            UnityEngine.Object.DestroyImmediate(target);
        }
    }

    static void ModeChanged(PlayModeStateChange mode)
    {
        int stage = SessionState.GetInt(Key, 0);
        if (mode != PlayModeStateChange.EnteredEditMode || stage == 0)
            return;
        PlayerSettings.runInBackground = SessionState.GetBool("MPRSmokeBackground", false);
        if (stage != 100)
            File.AppendAllText("Migration/play-validation.txt", "CANCELLED — Play Mode stopped before completion\n");
        SessionState.SetInt(Key, 0);
        if (Application.isBatchMode)
        {
            string result = File.ReadAllText("Migration/play-validation.txt");
            EditorApplication.Exit(result.Contains("COMPLETE —") && !result.Contains("FAIL ") ? 0 : 1);
        }
    }
}
