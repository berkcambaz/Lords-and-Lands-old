using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add building states
public class BuildingSlot 
{
    public Province province;

    public Building building;

    public BuildingSlot(Province _province)
    {
        province = _province;
    }
    public void AddEffects()
    {
        if (building != null) building.AddEffects(province);
    }

    public void RemoveEffects()
    {
        if (building != null) building.RemoveEffects(province);
    }

    public float GetDefensive(Country _country)
    {
        // If there is no building
        if (building == null) return 0;

        // If building is not yours or not occupied by you
        if (province.owner.id != _country.id || (province.occupier != null && province.occupier.id != _country.id)) return 0;

        return building.defensive;
    }

    public float GetOffensive(Country _country)
    {
        // If there is no building
        if (building == null) return 0;

        // If building is not yours or not occupied by you
        if (province.owner.id != _country.id || (province.occupier != null && province.occupier.id != _country.id)) return 0;

        return building.offensive;
    }

    public float GetResistance()
    {
        if (building == null) return 0;
        return building.resistance;
    }

    public void Build(Building _building)
    {
        if (!CanBuild(_building)) return;
        _building.Build(province);

        TilemapManager.UpdateProvinceTile(province);

        // Update the dynamic panel & stat panel
        UIStatPanel.UpdateCountryStats(province.owner);
        //UIDynamicPanel.ShowProvince(province);
        UIDynamicPanel.UpdateBuilding();
    }

    public bool AvailableToBuild(Building _building)
    {
        // If not the owner
        if (Gameplay.currentCountry.id != province.owner.id) return false;

        // If prequisites are not met
        if (!_building.AvailableToBuild(province)) return false;

        // If there is already a building 
        if (building != null) return false;

        return true;
    }

    public bool CanBuild(Building _building)
    {
        // If prequisites are not met
        if (!_building.CanBuild(province)) return false;

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
        //UIDynamicPanel.ShowProvince(province);
        UIDynamicPanel.UpdateDiplomacy();
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
