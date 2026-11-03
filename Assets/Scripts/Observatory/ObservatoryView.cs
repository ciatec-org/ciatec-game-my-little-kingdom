using UnityEngine;
using UnityEngine.UI;

public sealed class ObservatoryView : MonoBehaviour
{
    public Button backButton;
    public Text objectName, objectType;
    public void ShowInformation(string title, string type)
    {
        objectName.text = title;
        objectType.text = type;
    }
}
