using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class GameUI : MonoBehaviour
{
    public RectTransform canvasRect, frame;
    public Image backdrop;
    public MenuView menu;
    public TutorialView tutorialFirst, tutorialSecond;
    public TrialView trial;
    public VillageHUD village;
    public ObservatoryView observatory;
    public void Show(GameScreen screen, int tutorialStep = 0)
    {
        menu.gameObject.SetActive(screen == GameScreen.Menu);
        tutorialFirst.gameObject.SetActive(screen == GameScreen.Tutorial && tutorialStep == 0);
        tutorialSecond.gameObject.SetActive(screen == GameScreen.Tutorial && tutorialStep == 1);
        trial.gameObject.SetActive(screen == GameScreen.Trial);
        village.gameObject.SetActive(screen == GameScreen.Village || screen == GameScreen.FinalVillage || screen == GameScreen.BuildingComplete);
        observatory.gameObject.SetActive(screen == GameScreen.Observatory);
        bool map = screen == GameScreen.Village || screen == GameScreen.FinalVillage || screen == GameScreen.BuildingComplete;
        backdrop.color = map ? Color.clear : new Color32(25, 31, 37, 255);
        backdrop.raycastTarget = !map;
        if (EventSystem.current != null)
        {
            var first = (screen == GameScreen.Menu ? menu.startButton : screen == GameScreen.Tutorial ? (tutorialStep == 0 ? tutorialFirst.nextButton : tutorialSecond.nextButton) : screen == GameScreen.Observatory ? observatory.backButton : null);
            EventSystem.current.SetSelectedGameObject(first != null ? first.gameObject : null);
        }
    }

    void Update()
    {
        float scale = Mathf.Min(canvasRect.rect.width / 1600f, canvasRect.rect.height / 900f);
        if (scale > 0)
            frame.localScale = Vector3.one * scale;
    }
}
