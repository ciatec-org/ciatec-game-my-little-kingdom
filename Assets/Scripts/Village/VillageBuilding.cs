using System;
using MeuPequenoReino.Core;
using UnityEngine;
using UnityEngine.UI;

public sealed class VillageBuilding : MonoBehaviour
{
    public BuildingDefinition definition;
    public bool isBridge;
    public GameObject completedArt, plotArt;
    public Transform entrance, doorway;
    public Button button;
    public Image labelBackground;
    public Text label;
    GameSession session;
    KingdomDefinition kingdom;
    Action<VillageBuilding> selected;
    bool bound;
    public bool IsCastle => definition == null;
    public int Index => IsCastle ? -1 : kingdom.IndexOf(definition);
    public bool Built => IsCastle || session.IsBuilt(Index);

    public void Initialize(GameSession gameSession, KingdomDefinition kingdomDefinition, Action<VillageBuilding> onSelected)
    {
        session = gameSession;
        kingdom = kingdomDefinition;
        selected = onSelected;
        if (!bound && button != null)
        {
            button.onClick.AddListener(Click);
            bound = true;
        }
    }

    void Click() => selected?.Invoke(this);

    public void Refresh(bool busy)
    {
        bool built = Built;
        completedArt.SetActive(built);
        plotArt.SetActive(!built);
        string status = IsCastle ? "PRONTO" : !session.IsUnlocked(Index) ? "CONSTRUA A " + definition.prerequisite.displayName.ToUpperInvariant() : built ? (definition.opensObservatory ? "ABRIR" : "PRONTO") : session.CurrentBuilding == Index ? $"EM ANDAMENTO {session.Pieces(Index)}/{definition.requiredPieces}" : "DISPONÍVEL";
        label.text = (IsCastle ? "CASTELO" : definition.displayName) + "\n" + status;
        bool allowed = !IsCastle && !busy && session.IsUnlocked(Index) && (built ? definition.opensObservatory : !session.Paused);
        if (button != null)
            button.interactable = allowed;
    }

    public void SetSelected(bool selected)
    {
        labelBackground.color = selected ? new Color32(75, 99, 144, 255) : new Color32(39, 54, 50, 232);
    }
}
