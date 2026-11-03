using System;
using UnityEngine;

[Serializable]
public sealed class TrialDefinition
{
    public TrialModel model;
    public float rotation;
    public Vector3 rotationAxis = Vector3.up;
    public bool structuralMatch;
}

[CreateAssetMenu(menuName = "Meu Pequeno Reino/Construção")]
public sealed class BuildingDefinition : ScriptableObject
{
    public string id;
    public string displayName;
    public int requiredPieces = 5;
    public BuildingDefinition prerequisite;
    public bool opensObservatory;
    public TrialDefinition[] trials;
}
