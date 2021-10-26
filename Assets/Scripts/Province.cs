using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province
{
    public ProvinceState state;

    public Country owner;
    public Country occupier;

    public BuildingSlot buildingSlot;
    public ArmySlot armySlot;

    public Vector2Int pos;

    public Province(ref Country _owner, Vector2Int _pos)
    {
        state = ProvinceState.Free;

        owner = _owner;

        buildingSlot = new BuildingSlot(this);
        armySlot = new ArmySlot(this);

        pos = _pos;
    }

    public void Conquer()
    {
        owner = armySlot.country;
        occupier = null;
        state = ProvinceState.Free;

        buildingSlot.AddEffects();
    }

    public void Siege()
    {
        occupier = armySlot.country;
        state = ProvinceState.Occupied;

        buildingSlot.RemoveEffects();
    }

    public void Unsiege()
    {
        occupier = null;
        state = ProvinceState.Free;

        buildingSlot.AddEffects();
    }

    public bool Actable(Country _country)
    {
        // Actable if there is no army, or an army that is not ours
        return armySlot.army == null || armySlot.country.id != _country.id;
    }
}

public enum ProvinceState
{
    /// <summary>
    /// A country directly owns the province.
    /// </summary>
    Free,
    /// <summary>
    /// Province has a army that doesn't belong to the owner.
    /// </summary>
    Attacked,
    /// <summary>
    /// Occupier and the owner of the province are different.
    /// </summary>
    Occupied
}