using System.Collections;
using MeuPequenoReino.Core;
using MeuPequenoReino.Telemetry;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Scene composition and screen transitions; session rules live in GameSession.</summary>
public sealed class GameApplication : MonoBehaviour
{
    public KingdomDefinition definition;
    public GameUI ui;
    public TrialController trials;
    public TutorialController tutorial;
    public GameSession Session { get; private set; }
    public VillageWorld Village { get; private set; }

    int selected = -1;
    bool initialized;
    TrialTelemetrySession telemetry;

    IEnumerator Start()
    {
        if (initialized)
            yield break;
        initialized = true;
        StartSession();
        ui.Show(GameScreen.Menu);
        ui.menu.portrait.Present(26, false);
        ui.menu.startButton.onClick.AddListener(StartTutorial);
        ui.observatory.backButton.onClick.AddListener(ReturnToVillage);
        ui.village.actionButton.onClick.AddListener(VillageAction);
        tutorial.Initialize(ui, definition.firstBuilding.trials[0].model, () => StartBuilding(definition.IndexOf(definition.firstBuilding)));
        ui.menu.startButton.interactable = false;
        if (!SceneManager.GetSceneByName(definition.villageScene).isLoaded)
            yield return SceneManager.LoadSceneAsync(definition.villageScene, LoadSceneMode.Additive);
        Village = FindFirstObjectByType<VillageWorld>(FindObjectsInactive.Include);
        if (Village == null)
            throw new System.InvalidOperationException("Cena da vila não contém VillageWorld.");
        Village.Initialize(Session, definition);
        Village.Selected = SelectBuilding;
        Village.gameObject.SetActive(false);
        ui.menu.startButton.interactable = true;
    }

    void StartTutorial()
    {
        if (Session.Screen != GameScreen.Menu)
            return;
        Session.Screen = GameScreen.Tutorial;
        tutorial.Play();
    }

    public void StartBuilding(int index)
    {
        if (Village.Busy || trials.Busy || !Session.StartBuilding(index))
            return;
        selected = -1;
        Village.gameObject.SetActive(false);
        trials.Present(false);
    }

    void FinishBuilding(int index)
    {
        StartCoroutine(ShowConstruction(index));
    }

    IEnumerator ShowConstruction(int index)
    {
        selected = -1;
        Session.Screen = GameScreen.BuildingComplete;
        Village.gameObject.SetActive(true);
        Village.Refresh();
        ui.Show(Session.Screen);
        RefreshHUD();
        yield return Village.Reveal(index);
        Session.Screen = Session.Complete ? GameScreen.FinalVillage : GameScreen.Village;
        RefreshHUD();
        if (Session.Complete)
            EndTelemetry();
    }

    public void ShowVillage()
    {
        Session.Screen = Session.Complete ? GameScreen.FinalVillage : GameScreen.Village;
        selected = -1;
        Village.gameObject.SetActive(true);
        Village.Refresh();
        ui.Show(Session.Screen);
        RefreshHUD();
    }

    void SelectBuilding(VillageBuilding building)
    {
        if (Village.Busy)
            return;
        if (building.Built)
        {
            if (building.definition.opensObservatory)
            {
                Session.Screen = GameScreen.Observatory;
                Village.gameObject.SetActive(false);
                ui.Show(Session.Screen);
            }

            return;
        }

        if (Session.Paused)
            return;
        selected = building.Index;
        foreach (var b in Village.buildings)
            b.SetSelected(b == building);
        RefreshHUD();
    }

    void ReturnToVillage()
    {
        ShowVillage();
    }

    void VillageAction()
    {
        if (Village.Busy)
            return;
        if (Session.Complete)
        {
            Restart();
            return;
        }

        if (Session.Paused)
        {
            Village.gameObject.SetActive(false);
            trials.Resume();
            return;
        }

        if (selected >= 0)
            StartBuilding(selected);
    }

    void RefreshHUD()
    {
        var hud = ui.village;
        hud.heading.text = $"MEU PEQUENO REINO   {Session.CompletedBuildings}/{definition.buildings.Length}";
        hud.actionButton.gameObject.SetActive(true);
        if (Session.Screen == GameScreen.BuildingComplete)
        {
            hud.message.text = "CONSTRUINDO...";
            hud.actionButton.gameObject.SetActive(false);
        }
        else if (Session.Complete)
        {
            hud.message.text = "REINO CONCLUÍDO!";
            hud.actionLabel.text = "REINICIAR";
        }
        else if (Session.Paused)
        {
            var b = definition.buildings[Session.CurrentBuilding];
            hud.message.text = $"{b.displayName}   {Session.Pieces(Session.CurrentBuilding)}/{b.requiredPieces}";
            hud.actionLabel.text = "RETOMAR";
        }
        else if (selected >= 0)
        {
            hud.message.text = definition.buildings[selected].displayName;
            hud.actionLabel.text = "INICIAR";
        }
        else
        {
            hud.message.text = "ESCOLHA UMA CONSTRUÇÃO";
            hud.actionButton.gameObject.SetActive(false);
        }
    }

    public void Restart()
    {
        StopAllCoroutines();
        EndTelemetry();
        StartSession();
        selected = -1;
        Village.ResetView();
        Village.Initialize(Session, definition);
        Village.gameObject.SetActive(false);
        foreach (var b in Village.buildings)
            b.SetSelected(false);
        ui.Show(GameScreen.Menu);
        ui.menu.portrait.Present(26, false);
    }

    void StartSession()
    {
        Session = definition.CreateSession();
        telemetry = new TrialTelemetrySession(Application.version, Application.persistentDataPath);
        trials.Initialize(Session, definition, ui, ShowVillage, FinishBuilding, telemetry);
    }

    void EndTelemetry() => telemetry?.End(Time.realtimeSinceStartupAsDouble);

    void OnApplicationQuit() => EndTelemetry();

    void OnDestroy() => EndTelemetry();
}
