using UnityEngine;
using UnityEngine.UI;

public sealed class CharacterPortrait : MonoBehaviour
{
    public PortraitLibrary library;
    public RectTransform figure;
    public Image[] layers;
    float elapsed;
    bool arriving;
    int lastFrame = -1;
    readonly int[] rows = new int[3];
    public bool HasArrived => !arriving || elapsed >= 1.08f;

    public void Present(int seed, bool animateArrival)
    {
        var random = new System.Random(unchecked(seed * 7919 + 17));
        rows[0] = random.Next(11);
        rows[1] = 11 + random.Next(10);
        rows[2] = 21 + random.Next(28);
        arriving = animateArrival;
        elapsed = 0;
        lastFrame = -1;
        Tick(0);
    }

    void Update()
    {
        Tick(Time.unscaledDeltaTime);
    }

    void Tick(float delta)
    {
        if (figure == null || library == null)
            return;
        elapsed += delta;
        float t = arriving ? Mathf.SmoothStep(0, 1, Mathf.Clamp01(elapsed / 1.08f)) : 1;
        figure.localScale = Vector3.one * Mathf.Lerp(.78f, 1, t);
        figure.anchoredPosition = new Vector2(0, Mathf.Lerp(-112, -170, t));
        int frame = HasArrived ? 0 : 1 + Mathf.FloorToInt(elapsed / .135f) % 6;
        if (frame == lastFrame)
            return;
        lastFrame = frame;
        for (int i = 0; i < layers.Length; i++)
            layers[i].sprite = library.rows[rows[i]].frames[frame];
    }
}
