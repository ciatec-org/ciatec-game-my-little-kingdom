using UnityEngine;

[CreateAssetMenu(menuName = "Meu Pequeno Reino/Reino")]
public sealed class KingdomDefinition : ScriptableObject
{
    public BuildingDefinition[] buildings;
    public BuildingDefinition firstBuilding;
    public string villageScene = "Village";
    public int IndexOf(BuildingDefinition building) => System.Array.IndexOf(buildings, building);
}
