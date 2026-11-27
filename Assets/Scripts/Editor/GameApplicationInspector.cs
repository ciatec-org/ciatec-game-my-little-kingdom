using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameApplication))]
public sealed class GameApplicationInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var app = (GameApplication)target;
        var session = app.Session;
        if (session == null)
            return;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Sessão atual", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Tela", session.Screen.ToString());
        EditorGUILayout.LabelField("Construções", session.CompletedBuildings + " / " + app.definition.buildings.Length);
        EditorGUILayout.LabelField("Pausa", session.Paused ? "Sim" : "Não");
        if (session.CurrentBuilding >= 0)
        {
            var building = app.definition.buildings[session.CurrentBuilding];
            EditorGUILayout.LabelField("Construção atual", building.displayName);
            EditorGUILayout.LabelField("Desafio", (session.CandidateIndex + 1) + " / " + building.trials.Length);
            EditorGUILayout.LabelField("Peças", session.Pieces(session.CurrentBuilding) + " / " + building.requiredPieces);
        }

        if (Application.isPlaying)
            Repaint();
    }
}
