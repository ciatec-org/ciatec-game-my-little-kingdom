using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public static class KingdomValidation
{
    static readonly List<string> checks = new List<string>();
    static int verifiedReferences;
    static Vector3Int OriginalCell(Vector2Int p) => new Vector3Int(-p.y, -p.x, 0);
    static Vector3 OriginalPoint(Vector2Int p) => new Vector3((p.x - p.y) * .5f, -(p.x + p.y) * .25f, 0);
    static void Reference(bool condition, string description)
    {
        if (!condition)
            throw new InvalidOperationException(description);
        verifiedReferences++;
    }

    static void Require(bool condition, string description)
    {
        if (!condition)
            throw new InvalidOperationException(description);
        checks.Add(description);
    }

    [MenuItem("Meu Pequeno Reino/Validar estrutura migrada")]
    public static void Run()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            throw new InvalidOperationException("Execute a validação estrutural fora do Play Mode.");
        Directory.CreateDirectory("Migration");
        checks.Clear();
        verifiedReferences = 0;
        var definition = AssetDatabase.LoadAssetAtPath<KingdomDefinition>("Assets/Data/Kingdom.asset");
        var original = JsonUtility.FromJson<OriginalKingdomContract>(File.ReadAllText("Documentation/OriginalTrialContract.json")).buildings;
        Require(definition != null && definition.buildings.Length == original.Length, "Five building definitions exist");
        for (int b = 0; b < original.Length; b++)
        {
            var building = definition.buildings[b];
            Require(building.id == original[b].id && building.requiredPieces == original[b].requiredPieces, "Building identity and target preserved: " + building.id);
            Require(building.trials.Length == 10, "Ten trials: " + building.id);
            for (int i = 0; i < 10; i++)
            {
                var before = original[b].trials[i];
                var after = building.trials[i];
                Require(after.structuralMatch == before.structuralMatch && after.rotation == before.rotation && after.rotationAxis == before.rotationAxis && after.model.modelId == before.modelId, "Condition, rotation and model preserved: " + before.modelId);
                Require(after.model.cells.SequenceEqual(before.cells), "Cell coordinates preserved: " + before.modelId);
                Require(after.model.referenceMesh != null && after.model.mirroredMesh != null && EditorUtility.IsPersistent(after.model.referenceMesh) && EditorUtility.IsPersistent(after.model.mirroredMesh), "Persistent reference/mirrored meshes: " + before.modelId);
                VerifyMesh(after.model.referenceMesh, before.cells);
                VerifyMesh(after.model.mirroredMesh, TrialMeshBaker.Mirror(before.cells));
            }
        }

        Require(definition.buildings[4].prerequisite == definition.buildings[3], "Bridge prerequisite preserved");
        var session = new GameSession(definition);
        Require(!session.IsUnlocked(4), "Mill locked initially");
        for (int b = 0; b < 5; b++)
        {
            Require(session.StartBuilding(b), "Building starts: " + b);
            int accepted = 0;
            for (int i = 0; i < 10; i++)
            {
                Require(session.CandidateIndex == i, "Trial order preserved: " + b + "/" + i);
                Require(!session.IsCorrect(!session.CurrentTrial.structuralMatch), "Wrong answer remains incorrect: " + b + "/" + i);
                int pieces = session.Pieces(b);
                Require(session.Pause() && !session.StartBuilding((b + 1) % 5) && session.Resume() && session.CandidateIndex == i && session.Pieces(b) == pieces, "Pause/resume preserves progress: " + b + "/" + i);
                if (session.CurrentTrial.structuralMatch)
                    accepted++;
                var result = session.ApplyCorrectAnswer();
                Require(session.Pieces(b) == accepted, "Pieces preserved: " + b + "/" + i);
                Require((result == AnswerResult.BuildingCompleted) == (i == 9), "Completion at final stimulus: " + b + "/" + i);
            }
        }

        Require(session.Complete && session.CompletedBuildings == 5, "Kingdom completion preserved");
        var fresh = new GameSession(definition);
        Require(fresh.CompletedBuildings == 0 && fresh.CurrentBuilding == -1 && !fresh.Paused, "Fresh session has no stale progress");
        foreach (string guid in AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefabs" }))
        {
            string file = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(file);
            ValidateObjects(prefab, file);
        }

        var previous = SceneManager.GetActiveScene();
        foreach (string file in new[]
        {
            "Assets/Scenes/Kingdom.unity",
            "Assets/Scenes/Village.unity"
        }

        )
        {
            var scene = EditorSceneManager.OpenPreviewScene(file);
            try
            {
                foreach (var root in scene.GetRootGameObjects())
                    ValidateObjects(root, file);
                if (file.EndsWith("Village.unity"))
                {
                    var world = scene.GetRootGameObjects().SelectMany(r => r.GetComponentsInChildren<VillageWorld>(true)).Single();
                    Require(world.navigation.grid.cellLayout == GridLayout.CellLayout.Isometric, "Native isometric Grid");
                    Require(world.navigation.paths.GetUsedTilesCount() > 0 && world.navigation.water.GetUsedTilesCount() > 0, "Serialized paths and water Tilemaps");
                    Require(world.buildings.Length == 6 && world.buildings.All(b => PrefabUtility.IsPartOfPrefabInstance(b)), "Six positioned building prefab instances");
                    foreach (var p in new[]
                    {
                        new Vector2Int(0, 0),
                        new Vector2Int(-12, -6),
                        new Vector2Int(8, -6),
                        new Vector2Int(-4, 14)
                    }

                    )
                        Require(Vector3.Distance(world.navigation.Point(OriginalCell(p)), OriginalPoint(p)) < .001f, "Isometric coordinates preserved: " + p);
                    var navigationSession = new GameSession(definition);
                    world.Initialize(navigationSession);
                    var from = OriginalCell(new Vector2Int(3, -3));
                    var mill = world.Find(definition.buildings[4]);
                    var target = world.navigation.Cell(mill.entrance.position);
                    Require(world.navigation.Route(from, target) == null, "No route to mill before bridge");
                    navigationSession.StartBuilding(3);
                    for (int i = 0; i < 10; i++)
                        navigationSession.ApplyCorrectAnswer();
                    world.Refresh();
                    Require(world.navigation.Route(from, target) != null, "Route to mill after bridge");
                }
            }
            finally
            {
                EditorSceneManager.ClosePreviewScene(scene);
            }
        }

        if (previous.IsValid())
            SceneManager.SetActiveScene(previous);
        File.WriteAllLines("Migration/static-validation.txt", checks.Prepend("PASS — " + checks.Count + " semantic checks; " + verifiedReferences + " serialized reference/script assertions"));
        Debug.Log("[Validation] PASS " + checks.Count + " checks");
    }

    static void VerifyMesh(Mesh mesh, Vector3Int[] cells)
    {
        var occupied = new HashSet<Vector3Int>(cells);
        var directions = new[]
        {
            Vector3Int.right,
            Vector3Int.left,
            Vector3Int.up,
            Vector3Int.down,
            new Vector3Int(0, 0, 1),
            new Vector3Int(0, 0, -1)
        };
        int faces = cells.Sum(c => directions.Count(d => !occupied.Contains(c + d)));
        Require(mesh.vertexCount == faces * 4 && mesh.triangles.Length == faces * 6, "Mesh external face count: " + mesh.name);
        var expected = TrialMeshBaker.BakeMesh(cells, out _);
        try
        {
            Require(mesh.vertices.SequenceEqual(expected.vertices) && mesh.normals.SequenceEqual(expected.normals) && mesh.triangles.SequenceEqual(expected.triangles), "Mesh geometry matches coordinates: " + mesh.name);
        }
        finally
        {
            UnityEngine.Object.DestroyImmediate(expected);
        }
    }

    static void ValidateObjects(GameObject root, string path)
    {
        foreach (var transform in root.GetComponentsInChildren<Transform>(true))
        {
            Reference(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(transform.gameObject) == 0, "No missing script: " + path + "/" + transform.name);
            foreach (var component in transform.GetComponents<Component>())
            {
                if (component == null)
                    continue;
                var serialized = new SerializedObject(component);
                var property = serialized.GetIterator();
                while (property.Next(true))
                    if (property.propertyType == SerializedPropertyType.ObjectReference)
                        Reference(property.objectReferenceValue != null || property.objectReferenceInstanceIDValue == 0, "No dangling reference: " + path + "/" + transform.name + "." + property.propertyPath);
            }
        }

        checks.Add("Valid serialized hierarchy: " + path + "/" + root.name);
    }
}

[Serializable]
public sealed class OriginalKingdomContract
{
    public OriginalBuildingContract[] buildings;
}

[Serializable]
public sealed class OriginalBuildingContract
{
    public string id;
    public string displayName;
    public int requiredPieces;
    public OriginalTrialContract[] trials;
}

[Serializable]
public sealed class OriginalTrialContract
{
    public int modelId;
    public Vector3Int[] cells;
    public bool structuralMatch;
    public float rotation;
    public Vector3 rotationAxis;
}
