using System.Collections.Generic;
using UnityEngine;

public sealed class VillageAnimal : MonoBehaviour
{
    public AnimalAnimation animationData;
    public SpriteRenderer picture;
    public Vector3 home;
    public Vector3 startPosition;
    public int seed;
    public float speed = .3f;
    VillageNavigation navigation;
    System.Random random;
    List<Vector3> route;
    Vector3Int node, homeCell;
    int next, direction;
    float wait, animation, pace;
    public void Initialize(VillageNavigation paths)
    {
        navigation = paths;
        ResetAnimal();
    }

    public void ResetAnimal()
    {
        random = new System.Random(seed);
        transform.position = startPosition;
        node = navigation.Cell(startPosition);
        homeCell = navigation.Cell(home);
        route = null;
        next = direction = 0;
        wait = 1 + (float)random.NextDouble() * 5;
        animation = pace = 0;
    }

    void Update()
    {
        if (navigation == null)
            return;
        float dt = Mathf.Min(Time.unscaledDeltaTime, .05f);
        bool moving = speed > 0 && route != null && next < route.Count;
        if (moving)
        {
            var delta = route[next] - transform.position;
            if (delta.sqrMagnitude > .000001f)
                direction = delta.y >= 0 ? (delta.x >= 0 ? 0 : 1) : (delta.x >= 0 ? 2 : 3);
            float remaining = Vector3.Distance(transform.position, route[route.Count - 1]);
            float target = speed * Mathf.Clamp01(remaining / (24f / 96));
            pace = Mathf.MoveTowards(pace, Mathf.Max(6f / 96, target), speed * 2 * dt);
            if (VillageNavigation.Walk(transform, route, ref next, pace * dt))
            {
                route = null;
                wait = 2 + (float)random.NextDouble() * 6;
                animation = pace = 0;
            }
            else
                animation += dt * 8 * (pace / speed);
        }
        else
        {
            wait -= dt;
            if (speed <= 0)
                animation += dt * 6;
            else if (wait <= 0)
            {
                route = navigation.AnimalRoute(node, homeCell, random, out var destination);
                node = destination;
                next = 1;
                animation = pace = 0;
                if (route == null)
                    wait = 2;
            }
        }

        var strip = animationData.directions[Mathf.Min(direction, animationData.directions.Length - 1)].frames;
        picture.sprite = strip[(int)animation % strip.Length];
    }
}
