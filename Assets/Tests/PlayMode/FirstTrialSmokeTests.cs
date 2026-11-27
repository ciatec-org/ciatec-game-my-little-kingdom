using System;
using System.Collections;
using System.IO;
using MeuPequenoReino.Core;
using MeuPequenoReino.Telemetry;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public sealed class FirstTrialSmokeTests
{
    GameObject root;
    KingdomDefinition kingdom;
    BuildingDefinition building;
    TrialModel model;
    PortraitLibrary portraits;
    Mesh mesh;
    string dataPath;
    TrialTelemetrySession telemetry;

    [UnityTest]
    public IEnumerator TutorialReachesFirstTrial()
    {
        root = new GameObject("First trial test", typeof(RectTransform));
        root.SetActive(false);
        portraits = ScriptableObject.CreateInstance<PortraitLibrary>();
        var ui = CreateUI();
        var tutorial = root.AddComponent<TutorialController>();
        var trials = root.AddComponent<TrialController>();
        var session = CreateKingdom().CreateSession();
        session.Screen = GameScreen.Tutorial;
        dataPath = Path.Combine(Path.GetTempPath(), $"MPR-FirstTrial-{Guid.NewGuid():N}");
        telemetry = new TrialTelemetrySession(Application.version, dataPath);
        trials.Initialize(session, kingdom, ui, null, null, telemetry);
        tutorial.Initialize(ui, model, () =>
        {
            Assert.That(session.StartBuilding(0), Is.True);
            trials.Present(false);
        });

        root.SetActive(true);
        tutorial.Play();
        Assert.That(ui.tutorialFirst.gameObject.activeInHierarchy, Is.True);
        yield return null;
        ui.tutorialFirst.nextButton.onClick.Invoke();
        Assert.That(ui.tutorialSecond.gameObject.activeInHierarchy, Is.True);
        yield return null;
        ui.tutorialSecond.nextButton.onClick.Invoke();

        Assert.That(session.Screen, Is.EqualTo(GameScreen.Trial));
        Assert.That(trials.Busy, Is.True);
        double deadline = Time.realtimeSinceStartupAsDouble + 10;
        while (trials.Busy && Time.realtimeSinceStartupAsDouble < deadline)
            yield return null;

        Assert.That(trials.Busy, Is.False, "The first trial never became interactive.");
        Assert.That(kingdom.GetTrial(session), Is.SameAs(building.trials[0]));
        Assert.That(ui.trial.reference.gameObject.activeInHierarchy, Is.True);
        Assert.That(ui.trial.candidate.gameObject.activeInHierarchy, Is.True);
        Assert.That(ui.trial.reference.meshFilter.sharedMesh, Is.SameAs(mesh));
        Assert.That(ui.trial.candidate.meshFilter.sharedMesh, Is.SameAs(mesh));
        Assert.That(ui.trial.servesButton.IsInteractable(), Is.True);
        Assert.That(ui.trial.rejectsButton.IsInteractable(), Is.True);
        LogAssert.NoUnexpectedReceived();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        telemetry?.End(Time.realtimeSinceStartupAsDouble);
        Object.Destroy(root);
        Object.Destroy(kingdom);
        Object.Destroy(building);
        Object.Destroy(model);
        Object.Destroy(portraits);
        Object.Destroy(mesh);
        yield return null;
        if (Directory.Exists(dataPath))
            Directory.Delete(dataPath, true);
    }

    KingdomDefinition CreateKingdom()
    {
        mesh = new Mesh
        {
            vertices = new[] { Vector3.zero, Vector3.right, Vector3.up },
            triangles = new[] { 0, 1, 2 }
        };
        model = ScriptableObject.CreateInstance<TrialModel>();
        model.referenceMesh = mesh;
        model.mirroredMesh = mesh;
        model.framingRadius = 1;
        building = ScriptableObject.CreateInstance<BuildingDefinition>();
        building.id = "smoke-test";
        building.requiredPieces = 1;
        building.trials = new[] { new TrialDefinition { model = model, structuralMatch = true } };
        kingdom = ScriptableObject.CreateInstance<KingdomDefinition>();
        kingdom.buildings = new[] { building };
        kingdom.firstBuilding = building;
        return kingdom;
    }

    GameUI CreateUI()
    {
        var ui = root.AddComponent<GameUI>();
        ui.canvasRect = ui.frame = (RectTransform)root.transform;
        ui.backdrop = AddUI<Image>(root.transform);
        ui.menu = AddUI<MenuView>(root.transform);
        ui.tutorialFirst = CreateTutorialView();
        ui.tutorialSecond = CreateTutorialView();
        ui.village = AddUI<VillageHUD>(root.transform);
        ui.observatory = AddUI<ObservatoryView>(root.transform);
        ui.trial = AddUI<TrialView>(root.transform);
        var trial = ui.trial;
        trial.servesButton = AddUI<Button>(trial.transform);
        trial.rejectsButton = AddUI<Button>(trial.transform);
        trial.visitButton = AddUI<Button>(trial.transform);
        trial.buildingName = AddUI<Text>(trial.transform);
        trial.pieceCount = AddUI<Text>(trial.transform);
        trial.feedback = AddUI<Text>(trial.transform);
        trial.candidatePanel = AddUI<Image>(trial.transform);
        trial.progress = Array.Empty<Image>();
        trial.portrait = CreatePortrait(trial.transform);
        trial.reference = CreateModelPresentation(trial.transform);
        trial.candidate = CreateModelPresentation(trial.transform);
        return ui;
    }

    TutorialView CreateTutorialView()
    {
        var view = AddUI<TutorialView>(root.transform);
        view.nextButton = AddUI<Button>(view.transform);
        view.backButton = AddUI<Button>(view.transform);
        view.portrait = CreatePortrait(view.transform);
        view.reference = CreateModelPresentation(view.transform);
        view.candidate = CreateModelPresentation(view.transform);
        return view;
    }

    CharacterPortrait CreatePortrait(Transform parent)
    {
        var portrait = AddUI<CharacterPortrait>(parent);
        portrait.figure = (RectTransform)portrait.transform;
        portrait.library = portraits;
        portrait.layers = Array.Empty<Image>();
        return portrait;
    }

    static ModelPresentation CreateModelPresentation(Transform parent)
    {
        var presentation = AddUI<ModelPresentation>(parent);
        presentation.surface = presentation.gameObject.AddComponent<RawImage>();
        presentation.rig = new GameObject("Model rig").transform;
        presentation.rig.SetParent(presentation.transform, false);
        var camera = new GameObject("Model camera", typeof(Camera)).GetComponent<Camera>();
        camera.transform.SetParent(presentation.rig, false);
        camera.transform.localPosition = Vector3.back * 5;
        presentation.modelCamera = camera;
        presentation.modelCamera.orthographic = true;
        var shape = new GameObject("Shape", typeof(MeshFilter), typeof(MeshRenderer));
        shape.transform.SetParent(presentation.rig, false);
        presentation.meshFilter = shape.GetComponent<MeshFilter>();
        presentation.meshRenderer = shape.GetComponent<MeshRenderer>();
        presentation.meshRenderer.sharedMaterial = GraphicsSettings.currentRenderPipeline.defaultMaterial;
        return presentation;
    }

    static T AddUI<T>(Transform parent) where T : Component
    {
        var child = new GameObject(typeof(T).Name, typeof(RectTransform));
        child.transform.SetParent(parent, false);
        return child.AddComponent<T>();
    }
}
