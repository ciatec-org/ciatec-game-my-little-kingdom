using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class VillageNavigation : MonoBehaviour
{
    public Grid grid;
    public Tilemap paths, water, obstacles;
    public VillageBuilding bridge;
    public VillageBuilding[] buildings;
    public Vector3Int[] crossingCells;
    public Rect worldBounds = new Rect(-21.875f, -13.541667f, 43.75f, 27.083334f);
    static readonly Vector3Int[] Neighbors =
    {
        Vector3Int.right,
        Vector3Int.left,
        Vector3Int.up,
        Vector3Int.down
    };
    public Vector3Int Cell(Vector3 world) => grid.WorldToCell(world);
    public Vector3 Point(Vector3Int cell) => grid.CellToWorld(cell);
    public bool Walkable(Vector3Int cell)
    {
        if (!paths.HasTile(cell) || obstacles.HasTile(cell))
            return false;
        foreach (var building in buildings)
            if (!building.isBridge && (Cell(building.transform.position) - cell).sqrMagnitude < 4)
                return false;
        if (!water.HasTile(cell))
            return true;
        if (bridge == null || !bridge.Built)
            return false;
        foreach (var crossing in crossingCells)
            if (crossing == cell)
                return true;
        return false;
    }

    public List<Vector3> Route(Vector3Int from, Vector3Int to)
    {
        var previous = new Dictionary<Vector3Int, Vector3Int>
        {
            {
                from,
                from
            }
        };
        var queue = new Queue<Vector3Int>();
        queue.Enqueue(from);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == to)
                break;
            foreach (var d in Neighbors)
            {
                var next = current + d;
                if (!Walkable(next) || previous.ContainsKey(next))
                    continue;
                previous[next] = current;
                queue.Enqueue(next);
            }
        }

        if (!previous.ContainsKey(to))
            return null;
        var result = new List<Vector3>();
        for (var at = to; at != from; at = previous[at])
            result.Add(Point(at));
        result.Add(Point(from));
        result.Reverse();
        return Smooth(result);
    }

    public bool AnimalCell(Vector3Int cell, Vector3Int home)
    {
        foreach (var building in buildings)
            if ((Cell(building.transform.position) - cell).sqrMagnitude < 13)
                return false;
        Vector3 p = Point(cell);
        return worldBounds.Contains(p) && (cell - home).sqrMagnitude <= 20 && !water.HasTile(cell) && !paths.HasTile(cell) && !obstacles.HasTile(cell);
    }

    public List<Vector3> AnimalRoute(Vector3Int from, Vector3Int home, System.Random random, out Vector3Int destination)
    {
        var previous = new Dictionary<Vector3Int, Vector3Int>
        {
            {
                from,
                from
            }
        };
        var queue = new Queue<Vector3Int>();
        var options = new List<Vector3Int>();
        queue.Enqueue(from);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if ((current - from).sqrMagnitude >= 2)
                options.Add(current);
            foreach (var d in Neighbors)
            {
                var next = current + d;
                if (!AnimalCell(next, home) || previous.ContainsKey(next))
                    continue;
                previous[next] = current;
                queue.Enqueue(next);
            }
        }

        destination = from;
        if (options.Count == 0)
            return null;
        destination = options[random.Next(options.Count)];
        var result = new List<Vector3>();
        for (var at = destination; at != from; at = previous[at])
            result.Add(Point(at));
        result.Add(Point(from));
        result.Reverse();
        return Smooth(result);
    }

    static List<Vector3> Smooth(List<Vector3> points)
    {
        if (points.Count < 3)
            return points;
        var smooth = new List<Vector3>
        {
            points[0]
        };
        for (int i = 1; i < points.Count - 1; i++)
        {
            var incoming = (points[i] - points[i - 1]).normalized;
            var outgoing = (points[i + 1] - points[i]).normalized;
            if (Vector3.Dot(incoming, outgoing) > .99f)
            {
                smooth.Add(points[i]);
                continue;
            }

            var a = points[i] - incoming * (8f / 96);
            var b = points[i] + outgoing * (8f / 96);
            smooth.Add(a);
            for (int j = 1; j <= 6; j++)
            {
                float t = j / 6f;
                smooth.Add((1 - t) * (1 - t) * a + 2 * (1 - t) * t * points[i] + t * t * b);
            }
        }

        smooth.Add(points[points.Count - 1]);
        return smooth;
    }

    public static bool Walk(Transform actor, List<Vector3> points, ref int next, float budget)
    {
        while (next < points.Count)
        {
            Vector3 delta = points[next] - actor.position;
            float length = delta.magnitude;
            if (length > budget)
            {
                actor.position += delta / length * budget;
                return false;
            }

            actor.position = points[next++];
            budget -= length;
        }

        return true;
    }
}
