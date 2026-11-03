using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
[RequireComponent(typeof(SortingGroup))]
public sealed class DepthSorter : MonoBehaviour
{
    public Transform feet;
    public float feetOffset;
    SortingGroup group;
    void Awake()
    {
        group = GetComponent<SortingGroup>();
    }

    void LateUpdate()
    {
        if (group == null)
            group = GetComponent<SortingGroup>();
        int order = Mathf.Clamp(Mathf.RoundToInt(-((feet != null ? feet.position.y : transform.position.y) + feetOffset) * 100), -32000, 32000);
        if (group.sortingOrder != order)
            group.sortingOrder = order;
    }
}
