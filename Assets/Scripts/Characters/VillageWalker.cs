using System.Collections.Generic;
using UnityEngine;

public sealed class VillageWalker : MonoBehaviour
{
    public Transform body;
    public SpriteRenderer[] pictures;
    public Vector3 startPosition;
    public int seed;
    public float speed = .4f;
    VillageNavigation navigation;
    VillageBuilding[] buildings;
    System.Random random;
    List<Vector3> route;
    VillageBuilding destination;
    Vector3 entrance, doorway;
    int next, phase = 4;
    float clock, wait, gait;
    Vector3Int node;
    public void Initialize(VillageNavigation paths, VillageBuilding[] destinations)
    {
        navigation = paths;
        buildings = destinations;
        ResetWalker();
    }

    public void ResetWalker()
    {
        random = new System.Random(seed);
        transform.position = startPosition;
        node = navigation.Cell(startPosition);
        phase = 4;
        route = null;
        clock = 0;
        wait = (float)random.NextDouble() * 3;
        gait = (float)random.NextDouble() * 6;
        Opacity(1);
    }

    void Choose()
    {
        var candidates = new List<VillageBuilding>();
        foreach (var b in buildings)
            if (b.Built && !b.isBridge)
                candidates.Add(b);
        destination = random.NextDouble() < .65 ? candidates[random.Next(candidates.Count)] : null;
        Vector3Int target;
        if (destination != null)
            target = navigation.Cell(destination.entrance.position);
        else
        {
            var spots = new List<Vector3Int>();
            foreach (var b in buildings)
                if (!b.isBridge || b.Built)
                    spots.Add(navigation.Cell(b.entrance.position));
            target = spots[random.Next(spots.Count)];
        }

        route = navigation.Route(node, target);
        if (route == null)
        {
            wait = 1;
            return;
        }

        node = target;
        entrance = navigation.Point(target);
        doorway = destination != null ? destination.doorway.position : entrance;
        next = 1;
        phase = 0;
        clock = 0;
    }

    void Update()
    {
        if (navigation == null)
            return;
        float dt = Mathf.Min(Time.unscaledDeltaTime, .05f);
        if (phase == 4)
        {
            wait -= dt;
            if (wait <= 0)
                Choose();
        }
        else if (phase == 0)
        {
            if (VillageNavigation.Walk(transform, route, ref next, speed * dt))
            {
                if (destination == null)
                {
                    phase = 4;
                    wait = 1 + (float)random.NextDouble() * 2;
                }
                else
                {
                    phase = 1;
                    clock = 0;
                }
            }
        }
        else if (phase == 1 || phase == 3)
        {
            float duration = Mathf.Max(.8f, Vector3.Distance(entrance, doorway) / speed);
            clock += dt;
            float t = Mathf.Clamp01(clock / duration);
            bool entering = phase == 1;
            float progress = entering ? t : 1 - t;
            transform.position = Vector3.Lerp(entrance, doorway, progress);
            Opacity(1 - Mathf.SmoothStep(0, 1, Mathf.InverseLerp(.65f, 1, progress)));
            if (t >= 1)
            {
                clock = 0;
                if (entering)
                {
                    phase = 2;
                    wait = 2 + (float)random.NextDouble() * 4;
                }
                else
                {
                    Opacity(1);
                    phase = 4;
                    wait = .3f;
                }
            }
        }
        else if (phase == 2)
        {
            wait -= dt;
            if (wait <= 0)
            {
                phase = 3;
                clock = 0;
            }
        }

        bool moving = phase == 0 || phase == 1 || phase == 3;
        gait += dt * 9;
        body.localPosition = new Vector3(0, moving ? Mathf.Abs(Mathf.Sin(gait)) * .8f / 96 : 0, 0);
        body.localRotation = Quaternion.Euler(0, 0, moving ? Mathf.Sin(gait) * 5 : 0);
    }

    void Opacity(float alpha)
    {
        foreach (var picture in pictures)
        {
            var color = picture.color;
            color.a = alpha;
            picture.color = color;
        }
    }
}
