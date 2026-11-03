using UnityEngine;

[CreateAssetMenu(menuName = "Meu Pequeno Reino/Modelo de desafio")]
public sealed class TrialModel : ScriptableObject
{
    public int modelId;
    public Vector3Int[] cells;
    public Mesh referenceMesh;
    public Mesh mirroredMesh;
    public float framingRadius;
}
