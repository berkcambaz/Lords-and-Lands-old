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

    public void Conquer(ArmySlot _armySlot)
    {
        // If this is the capital
        if (buildingSlot.building != null && buildingSlot.building.id == BuildingId.Capital)
        {
            // If there is only one province left, allow it to be conquered
            if (Utility.GetProvinceCount(owner) != 1) return;

            // Remove the capital building
            buildingSlot.building.Demolish(this);

            // TODO: Remove country

            TilemapManager.UpdateProvinceTile(this);
        }

        owner = _armySlot.country;
        occupier = null;
        state = ProvinceState.Free;

        buildingSlot.AddEffects();
    }

    public void Siege(ArmySlot _armySlot)
    {
        // If this is the capital
        if (buildingSlot.building != null && buildingSlot.building.id == BuildingId.Capital)
        {
            // Randomly siege provinces of that nation, excluding the capital
            for (int i = 0; i < World.provinces.Length; ++i)
            {
                Province province = World.provinces[i];

                // If province is not occupied or occupied by the army on the province
                if (province.owner.id == owner.id && (province.occupier == null || province.occupier.id == armySlot.country.id))
                {
                    // If capital, don't siege since it will cause a recursive sieging bug
                    if (province.buildingSlot.building != null && province.buildingSlot.building.id == BuildingId.Capital)
                        continue;

                    // Dice & if the dice is higher than 3, siege that province, if already sieged, conquer
                    // TODO: Make 3 a constant
                    int dice = Gameplay.srandom.Dice();
                    if (dice > 3)
                    {
                        if (province.state == ProvinceState.Occupied) province.Conquer(_armySlot);
                        else province.Siege(_armySlot);
                        TilemapManager.UpdateProvinceTile(province);
                    }
                }
            }

            // If there is only one province left, allow it to be sieged
            if (Utility.GetProvinceCount(owner) != 1) return;
        }

        occupier = _armySlot.country;
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