using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province
{
    public ProvinceState state;
    public Country owner;
    public Country occupier;

    public Building building;

    public Army army;

    public Vector2Int pos;

    public Province(ref Country _owner, Vector2Int _pos)
    {
        state = ProvinceState.Free;

        owner = _owner;
        pos = _pos;
    }

    public void Update()
    {

    }

    public float GetOffensive()
    {
        if (building == null) return 0f;

        return building.offensive;
    }
    public float GetDefensive()
    {
        if (building == null) return 0f;

        return building.defensive;
    }
    public float GetResistance()
    {
        if (building == null) return 0f;

        return building.resistance;
    }

    public void Build(Building _building)
    {
        if (!CanBuild(_building)) return;

        building = _building;

        TilemapManager.UpdateProvinceTile(pos, this);

        // Update the dynamic panel & stat panel
        UIStatPanel.UpdateCountryStats(owner);
        UIDynamicPanel.ShowProvince(this);
    }

    public bool AvailableToBuild(Building _building)
    {
        // If prequisites are not met
        if (!_building.AvailableToBuild(owner)) return false;

        // If there is already a building 
        if (building != null) return false;

        return true;
    }

    public bool CanBuild(Building _building)
    {
        // If prequisites are not met
        if (!_building.CanBuild(owner)) return false;

        // If province is not free
        if (state != ProvinceState.Free) return false;

        return true;
    }

    public void Demolish()
    {
        if (!CanDemolish()) return;

        building = null;

        TilemapManager.UpdateProvinceTile(pos, this);

        // Update the dynamic panel & stat panel
        UIStatPanel.UpdateCountryStats(owner);
        UIDynamicPanel.ShowProvince(this);
    }

    public bool AvailableToDemolish()
    {
        // If prequisites are not met
        if (building != null && !building.AvailableToDemolish()) return false;

        // If there is no building
        if (building == null) return false;

        return true;
    }

    public bool CanDemolish()
    {
        // If prequisites are not met
        if (building != null && !building.CanDemolish()) return false;

        // If province is not free
        if (state != ProvinceState.Free) return false;

        return true;
    }

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