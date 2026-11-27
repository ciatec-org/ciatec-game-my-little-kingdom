using System;
using System.Collections;
using MeuPequenoReino.Core;
using UnityEngine;

public sealed class VillageWorld : MonoBehaviour
{
    public VillageNavigation navigation;
    public VillageCameraDrag cameraDrag;
    public VillageBuilding[] buildings;
    public VillageWalker[] villagers;
    public VillageAnimal[] animals;
    public GameSession Session { get; private set; }
    public bool Busy { get; private set; }
    KingdomDefinition definition;

    public Action<VillageBuilding> Selected;
    public void Initialize(GameSession session, KingdomDefinition kingdom)
    {
        Session = session;
        definition = kingdom;
        foreach (var building in buildings)
        {
            building.Initialize(session, kingdom, Select);
        }

        foreach (var walker in villagers)
            walker.Initialize(navigation, buildings);
        foreach (var animal in animals)
            animal.Initialize(navigation);
        Refresh();
    }

    public void Refresh()
    {
        foreach (var building in buildings)
            building.Refresh(Busy);
        cameraDrag.Locked = Busy;
    }

    public void Select(VillageBuilding building)
    {
        if (Busy || cameraDrag.DraggedThisGesture || building.IsCastle || !Session.IsUnlocked(building.Index))
            return;
        if (Session.Paused && !building.Built)
            return;
        Selected?.Invoke(building);
    }

    public VillageBuilding Find(BuildingDefinition definition)
    {
        foreach (var building in buildings)
            if (building.definition == definition)
                return building;
        return null;
    }

    public IEnumerator Reveal(int index)
    {
        Busy = true;
        Refresh();
        var building = Find(definition.buildings[index]);
        cameraDrag.Focus((Vector2)building.transform.position + new Vector2(0, 55f / 96));
        float elapsed = 0;
        var art = building.completedArt.transform;
        while (elapsed < .7f)
        {
            elapsed += Time.unscaledDeltaTime;
            float n = Mathf.Clamp01(elapsed / .7f);
            float scale = n < .75f ? Mathf.Lerp(.2f, 1.08f, n / .75f) : Mathf.Lerp(1.08f, 1, (n - .75f) / .25f);
            art.localScale = Vector3.one * scale;
            yield return null;
        }

        art.localScale = Vector3.one;
        Busy = false;
        Refresh();
    }

    public void ResetView()
    {
        StopAllCoroutines();
        Busy = false;
        cameraDrag.Focus(Vector2.zero);
        foreach (var building in buildings)
            building.completedArt.transform.localScale = Vector3.one;
    }
}
