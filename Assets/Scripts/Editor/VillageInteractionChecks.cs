using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

/// <summary>Real player-loop input, movement and editable-Tilemap checks in an isolated test run.</summary>
[InitializeOnLoad]
public static class VillageInteractionChecks
{
    const string Key = "MPRInteractionStage";
    static Mouse mouse;
    static Vector2 start, drag;
    static Vector3 cameraStart;
    static Vector3[] walkerPositions;
    static Vector3[] animalPositions;
    static Sprite[] animalFrames;
    static double nextTick, stageStarted;
    static VillageInteractionChecks()
    {
        Install();
    }

    [InitializeOnEnterPlayMode]
    static void Enter(EnterPlayModeOptions options)
    {
        Install();
    }

    static void Install()
    {
        EditorApplication.update -= Tick;
        EditorApplication.update += Tick;
        EditorApplication.playModeStateChanged -= Stopped;
        EditorApplication.playModeStateChanged += Stopped;
    }

    public static void RunWithStructure()
    {
        KingdomValidation.Run();
        Run();
    }

    [MenuItem("Meu Pequeno Reino/Verificar interação da vila em Play Mode")]
    public static void Run()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            throw new InvalidOperationException("Execute a verificação a partir do modo de edição.");
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            if (UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).isDirty)
                throw new InvalidOperationException("Salve as cenas modificadas antes de executar esta verificação.");
        Directory.CreateDirectory("Migration");
        File.WriteAllText("Migration/interaction-validation.txt", "RUNNING\n");
        SessionState.SetInt(Key, 1);
        SessionState.SetFloat("MPRInteractionStart", (float)EditorApplication.timeSinceStartup);
        SessionState.SetBool("MPRInteractionBackground", PlayerSettings.runInBackground);
        EditorApplication.isPaused = false;
        Application.runInBackground = true;
        EditorSceneManager.OpenScene("Assets/Scenes/Kingdom.unity");
        EditorApplication.isPlaying = true;
    }

    static void Check(bool value, string description)
    {
        if (!value)
            throw new InvalidOperationException(description);
        File.AppendAllText("Migration/interaction-validation.txt", "PASS " + description + "\n");
    }

    static void Stage(int stage)
    {
        SessionState.SetInt(Key, stage);
        stageStarted = EditorApplication.timeSinceStartup;
    }

    static void State(Vector2 point, bool held)
    {
        mouse.MakeCurrent();
        var state = new MouseState
        {
            position = point
        };
        if (held)
            state = state.WithButton(MouseButton.Left);
        InputSystem.QueueStateEvent(mouse, state);
    }

    static void Tick()
    {
        int stage = SessionState.GetInt(Key, 0);
        if (stage == 0 || stage == 100)
            return;
        EditorApplication.QueuePlayerLoopUpdate();
        if (EditorApplication.isPaused)
            EditorApplication.isPaused = false;
        if (!EditorApplication.isPlaying || EditorApplication.timeSinceStartup < nextTick)
            return;
        nextTick = EditorApplication.timeSinceStartup + .1;
        try
        {
            if (EditorApplication.timeSinceStartup - SessionState.GetFloat("MPRInteractionStart", 0) > 90)
                throw new TimeoutException("Interaction test timed out.");
            var app = UnityEngine.Object.FindFirstObjectByType<GameApplication>();
            if (app == null || app.Village == null)
                return;
            Application.runInBackground = true;
            var world = app.Village;
            double elapsed = EditorApplication.timeSinceStartup - stageStarted;
            if (stage == 1)
            {
                app.ShowVillage();
                Check(world.GetComponentsInChildren<Canvas>(true).All(c => c.worldCamera == world.cameraDrag.mapCamera), "World-space UI cameras are serialized correctly");
                mouse = InputSystem.AddDevice<Mouse>("KingdomTestMouse");
                mouse.MakeCurrent();
                cameraStart = world.cameraDrag.transform.position;
                start = new Vector2(Screen.width * .5f, Screen.height * .5f);
                drag = start + new Vector2(120, 0);
                walkerPositions = world.villagers.Select(v => v.transform.position).ToArray();
                animalPositions = world.animals.Select(a => a.transform.position).ToArray();
                animalFrames = world.animals.Select(a => a.picture.sprite).ToArray();
                State(start, true);
                Stage(2);
            }
            else if (stage == 2 && elapsed > .25)
            {
                State(drag, true);
                Stage(3);
            }
            else if (stage == 3 && elapsed > .35)
            {
                Check(world.cameraDrag.DraggedThisGesture && Vector3.Distance(cameraStart, world.cameraDrag.transform.position) > .01f, "Synthetic mouse drag moves native world camera");
                State(drag, false);
                Stage(4);
            }
            else if (stage == 4 && elapsed > .25)
            {
                world.cameraDrag.Locked = true;
                cameraStart = world.cameraDrag.transform.position;
                State(start, true);
                Stage(5);
            }
            else if (stage == 5 && elapsed > .25)
            {
                State(drag, true);
                Stage(6);
            }
            else if (stage == 6 && elapsed > .35)
            {
                Check(Vector3.Distance(cameraStart, world.cameraDrag.transform.position) < .0001f, "Construction camera lock blocks drag");
                State(drag, false);
                world.cameraDrag.Locked = false;
                var nav = world.navigation;
                var cell = nav.Cell(world.villagers[0].startPosition);
                var tile = nav.paths.GetTile(cell);
                Check(tile != null && nav.Walkable(cell), "Serialized path cell is initially navigable");
                nav.paths.SetTile(cell, null);
                Check(!nav.Walkable(cell), "Removing a native path tile immediately changes navigation");
                nav.paths.SetTile(cell, tile);
                Check(nav.Walkable(cell), "Restoring path tile restores navigation");
                var building = world.buildings.First(b => b.definition != null && !b.isBridge);
                Vector3 p = building.transform.position, entrance = building.entrance.position;
                building.transform.position = p + Vector3.right;
                Check(Vector3.Distance(building.entrance.position, entrance + Vector3.right) < .0001f, "Moving building transform moves its configured entrance");
                building.transform.position = p;
                Stage(7);
            }
            else if (stage == 7 && elapsed > 8)
            {
                Check(world.villagers.Where((v, i) => Vector3.Distance(v.transform.position, walkerPositions[i]) > .01f).Any(), "Villagers walk in the real player loop");
                Check(world.animals.Where((a, i) => a.speed > 0 && Vector3.Distance(a.transform.position, animalPositions[i]) > .01f).Any(), "Mobile animals walk in their habitats");
                Check(world.animals.Where((a, i) => a.picture.sprite != animalFrames[i]).Any(), "Animal animation frames advance");
                world.cameraDrag.Focus(Vector2.zero);
                var bridge = world.buildings.First(b => b.isBridge);
                start = world.cameraDrag.mapCamera.WorldToScreenPoint(bridge.labelBackground.rectTransform.TransformPoint(bridge.labelBackground.rectTransform.rect.center));
                State(start, false);
                Stage(8);
            }
            else if (stage == 8 && elapsed > .25)
            {
                State(start, true);
                Stage(9);
            }
            else if (stage == 9 && elapsed > .25)
            {
                State(start, false);
                Stage(10);
            }
            else if (stage == 10 && elapsed > .35)
            {
                var bridge = world.buildings.First(b => b.isBridge);
                Check(app.ui.village.actionButton.gameObject.activeInHierarchy && app.ui.village.message.text == bridge.definition.displayName, "Mouse click selects a construction through its world-space UI");
                Canvas.ForceUpdateCanvases();
                var buttonRect = (RectTransform)app.ui.village.actionButton.transform;
                start = RectTransformUtility.WorldToScreenPoint(null, buttonRect.TransformPoint(buttonRect.rect.center));
                State(start, false);
                Stage(11);
            }
            else if (stage == 11 && elapsed > .25)
            {
                State(start, true);
                Stage(12);
            }
            else if (stage == 12 && elapsed > .25)
            {
                State(start, false);
                Stage(13);
            }
            else if (stage == 13 && elapsed > .35)
            {
                var bridge = world.buildings.First(b => b.isBridge);
                Check(app.Session.Screen == GameScreen.Trial && app.Session.CurrentBuilding == app.Session.Definition.IndexOf(bridge.definition), "Mouse click on HUD starts selected construction");
                File.AppendAllText("Migration/interaction-validation.txt", "COMPLETE — input and village interactions passed\n");
                Finish();
            }
        }
        catch (Exception exception)
        {
            File.AppendAllText("Migration/interaction-validation.txt", "FAIL " + exception + "\n");
            Finish();
        }
    }

    static void Finish()
    {
        if (mouse != null)
        {
            InputSystem.RemoveDevice(mouse);
            mouse = null;
        }

        Stage(100);
        EditorApplication.isPlaying = false;
    }

    static void Stopped(PlayModeStateChange mode)
    {
        int stage = SessionState.GetInt(Key, 0);
        if (mode != PlayModeStateChange.EnteredEditMode || stage == 0)
            return;
        if (mouse != null)
        {
            if (InputSystem.devices.Contains(mouse))
                InputSystem.RemoveDevice(mouse);
            mouse = null;
        }
        PlayerSettings.runInBackground = SessionState.GetBool("MPRInteractionBackground", false);
        if (stage != 100)
            File.AppendAllText("Migration/interaction-validation.txt", "CANCELLED — Play Mode stopped before completion\n");
        SessionState.SetInt(Key, 0);
        if (Application.isBatchMode)
        {
            string result = File.ReadAllText("Migration/interaction-validation.txt");
            EditorApplication.Exit(result.Contains("COMPLETE —") && !result.Contains("FAIL ") ? 0 : 1);
        }
    }
}
