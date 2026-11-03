using UnityEngine;
using UnityEngine.UI;

public sealed class TrialView : MonoBehaviour
{
    public Button servesButton, rejectsButton, visitButton;
    public Text buildingName, pieceCount, feedback;
    public Image candidatePanel;
    public Image[] progress;
    public CharacterPortrait portrait;
    public ModelPresentation reference, candidate;
    public void SetInteraction(bool enabled)
    {
        servesButton.interactable = rejectsButton.interactable = visitButton.interactable = enabled;
    }

    public void ShowProgress(int accepted, int total)
    {
        pieceCount.text = accepted + " de " + total + " peças";
        for (int i = 0; i < progress.Length; i++)
            progress[i].color = i < accepted ? new Color32(199, 167, 104, 255) : new Color32(32, 49, 52, 255);
    }
}
