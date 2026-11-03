using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>Editor-only mesh authoring; there is no geometry generation in the player.</summary>
public static class TrialMeshBaker
{
    public static Vector3Int[] Mirror(Vector3Int[] original)
    {
        var result = new Vector3Int[original.Length];
        int maxX = 0;
        foreach (var p in original)
            maxX = Mathf.Max(maxX, p.x);
        for (int i = 0; i < result.Length; i++)
            result[i] = new Vector3Int(maxX - original[i].x, original[i].y, original[i].z);
        return result;
    }

    public static Mesh BakeMesh(Vector3Int[] cells, out float radius)
    {
        Vector3 min = cells[0], max = cells[0];
        foreach (var p in cells)
        {
            min = Vector3.Min(min, p);
            max = Vector3.Max(max, p);
        }

        Vector3 center = (min + max) * .5f;
        radius = 0;
        foreach (var p in cells)
            radius = Mathf.Max(radius, ((Vector3)p - center).magnitude + .87f);
        var occupied = new HashSet<Vector3Int>(cells);
        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var triangles = new List<int>();
        var uv = new List<Vector2>();
        var directions = new[]
        {
            Vector3Int.right,
            Vector3Int.left,
            Vector3Int.up,
            Vector3Int.down,
            new Vector3Int(0, 0, 1),
            new Vector3Int(0, 0, -1)
        };
        foreach (var cell in cells)
            foreach (var axis in directions)
            {
                if (occupied.Contains(cell + axis))
                    continue;
                Vector3 n = axis, u = Mathf.Abs(n.y) > .5f ? Vector3.right : Vector3.up, v = Vector3.Cross(n, u);
                Vector3 middle = (Vector3)cell - center + n * .5f;
                int start = vertices.Count;
                vertices.AddRange(new[] { middle - u * .5f - v * .5f, middle + u * .5f - v * .5f, middle + u * .5f + v * .5f, middle - u * .5f + v * .5f });
                normals.AddRange(new[] { n, n, n, n });
                uv.AddRange(new[] { Vector2.zero, Vector2.right, Vector2.one, Vector2.up });
                triangles.AddRange(new[] { start, start + 1, start + 2, start + 2, start + 3, start });
            }

        var mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uv);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateBounds();
        return mesh;
    }
}

[CustomEditor(typeof(TrialModel))]
public sealed class TrialModelInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("As malhas são assets. Alterar coordenadas exige regeneração explícita; os gabaritos não são alterados.", MessageType.Info);
        if (!GUILayout.Button("Regerar malhas a partir das coordenadas"))
            return;
        var model = (TrialModel)target;
        if (model.cells == null || model.cells.Length == 0)
        {
            Debug.LogError("Modelo sem coordenadas.");
            return;
        }

        var reference = TrialMeshBaker.BakeMesh(model.cells, out float radius);
        var mirror = TrialMeshBaker.BakeMesh(TrialMeshBaker.Mirror(model.cells), out _);
        if (model.referenceMesh != null)
        {
            reference.name = model.referenceMesh.name;
            EditorUtility.CopySerialized(reference, model.referenceMesh);
            Object.DestroyImmediate(reference);
        }
        else
        {
            model.referenceMesh = reference;
            AssetDatabase.AddObjectToAsset(reference, model);
        }

        if (model.mirroredMesh != null)
        {
            mirror.name = model.mirroredMesh.name;
            EditorUtility.CopySerialized(mirror, model.mirroredMesh);
            Object.DestroyImmediate(mirror);
        }
        else
        {
            model.mirroredMesh = mirror;
            AssetDatabase.AddObjectToAsset(mirror, model);
        }

        model.framingRadius = radius;
        EditorUtility.SetDirty(model);
        EditorUtility.SetDirty(model.referenceMesh);
        EditorUtility.SetDirty(model.mirroredMesh);
        AssetDatabase.SaveAssets();
    }
}
