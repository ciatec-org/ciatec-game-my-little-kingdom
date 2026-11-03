using System;
using System.Collections;
using UnityEngine;

public sealed class TutorialController : MonoBehaviour
{
    GameUI ui;
    TrialModel model;
    Action completed;
    bool bound;
    int step;
    public void Initialize(GameUI gameUI, TrialModel firstModel, Action onCompleted)
    {
        ui = gameUI;
        model = firstModel;
        completed = onCompleted;
        if (bound)
            return;
        bound = true;
        ui.tutorialFirst.nextButton.onClick.AddListener(Next);
        ui.tutorialSecond.nextButton.onClick.AddListener(Next);
        ui.tutorialSecond.backButton.onClick.AddListener(Back);
    }

    public void Play()
    {
        step = 0;
        ShowStep();
    }

    void Back()
    {
        step = 0;
        ShowStep();
    }

    void Next()
    {
        if (step == 0)
        {
            step = 1;
            ShowStep();
        }
        else
        {
            StopAllCoroutines();
            completed?.Invoke();
        }
    }

    void ShowStep()
    {
        StopAllCoroutines();
        ui.Show(GameScreen.Tutorial, step);
        var view = step == 0 ? ui.tutorialFirst : ui.tutorialSecond;
        view.portrait.Present(19, false);
        view.reference.Present(model, false, Quaternion.identity);
        view.candidate.Present(model, step == 1, Quaternion.AngleAxis(135, Vector3.up));
        StartCoroutine(Demonstrate(view.candidate));
    }

    IEnumerator Demonstrate(ModelPresentation presentation)
    {
        yield return new WaitForSecondsRealtime(.7f);
        float elapsed = 0;
        while (elapsed < 2.4f)
        {
            elapsed += Time.unscaledDeltaTime;
            presentation.SetPose(Quaternion.AngleAxis(Mathf.Lerp(135, 0, Mathf.SmoothStep(0, 1, elapsed / 2.4f)), Vector3.up));
            yield return null;
        }

        presentation.SetPose(Quaternion.identity);
    }
}
