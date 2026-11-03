using UnityEngine;
using UnityEngine.EventSystems;

public sealed class CelestialTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ObservatoryView view;
    public string objectName, objectType;
    public void OnPointerEnter(PointerEventData data)
    {
        view.ShowInformation(objectName, objectType);
        transform.localScale = Vector3.one * 1.12f;
    }

    public void OnPointerExit(PointerEventData data)
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerClick(PointerEventData data)
    {
        view.ShowInformation(objectName, objectType);
    }

    void OnDisable()
    {
        transform.localScale = Vector3.one;
    }
}
