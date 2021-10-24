using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add building states
public class BuildingSlot 
{
    public Building building;

    public Province province;

    public BuildingSlot(Province _province)
    {
        province = _province;
    }

    public void Build(Building _building)
    {
        if (!CanBuild(_building)) return;
        _building.Build(province);

        TilemapManager.UpdateProvinceTile(province);

        // Update the dynamic panel & stat panel
        UIStatPanel.UpdateCountryStats(province.owner);
        UIDynamicPanel.ShowProvince(province);
    }

    public bool AvailableToBuild(Building _building)
    {
        // If not the owner
        if (Gameplay.currentCountry.id != province.owner.id) return false;

        // If prequisites are not met
        if (!_building.AvailableToBuild(province.owner)) return false;

        // If there is already a building 
        if (building != null) return false;

        return true;
    }

    public bool CanBuild(Building _building)
    {
        // If prequisites are not met
        if (!_building.CanBuild(province.owner)) return false;

        // If province is not free
        if (province.state != ProvinceState.Free) return false;

        return true;
    }

    public void Demolish()
    {
        if (!CanDemolish()) return;
        building.Demolish(province);

        TilemapManager.UpdateProvinceTile(province);

        // Update the dynamic panel & stat panel
        UIStatPanel.UpdateCountryStats(province.owner);
        UIDynamicPanel.ShowProvince(province);
    }

    public bool AvailableToDemolish()
    {
        // If not the owner
        if (Gameplay.currentCountry.id != province.owner.id) return false;

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
        if (province.state != ProvinceState.Free) return false;

        return true;
    }
}
