using System;
using MeuPequenoReino.Core;
using UnityEngine;

[CreateAssetMenu(menuName = "Meu Pequeno Reino/Reino")]
public sealed class KingdomDefinition : ScriptableObject
{
    public BuildingDefinition[] buildings;
    public BuildingDefinition firstBuilding;
    public string villageScene = "Village";
    public int IndexOf(BuildingDefinition building) => Array.IndexOf(buildings, building);

    public GameSession CreateSession()
    {
        var rules = new BuildingRules[buildings?.Length ?? 0];
        for (int i = 0; i < rules.Length; i++)
        {
            var building = buildings[i];
            int? prerequisite = building.prerequisite == null ? (int?)null : IndexOf(building.prerequisite);
            var matches = Array.ConvertAll(building.trials, trial => trial.structuralMatch);
            rules[i] = new BuildingRules(building.requiredPieces, prerequisite, matches);
        }
        return new GameSession(rules);
    }

    public TrialDefinition GetTrial(GameSession session) => buildings[session.CurrentBuilding].trials[session.CandidateIndex];
}
