using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province
{
    public ProvinceState state;

    public Country owner;
    public Country occupier;

    public BuildingSlot buildingSlot;

    public Army army;

    public Vector2Int pos;

    public Province(ref Country _owner, Vector2Int _pos)
    {
        state = ProvinceState.Free;

        owner = _owner;

        buildingSlot = new BuildingSlot(this);

        pos = _pos;
    }

    //public float GetOffensive()
    //{
    //    if (building == null) return 0f;
    //
    //    return building.offensive;
    //}
    //public float GetDefensive()
    //{
    //    if (building == null) return 0f;
    //
    //    return building.defensive;
    //}
    //public float GetResistance()
    //{
    //    if (building == null) return 0f;
    //
    //    return building.resistance;
    //}

    public void Free()
    {

    }

    public void Attack()
    {

    }

    public void Occupy()
    {

    }

    public void Surrender()
    {

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