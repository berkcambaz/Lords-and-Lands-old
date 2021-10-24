using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmySlot 
{
    public Province province;

    public Army army;

    public GameObject gameobject;
    public ArmyState state;
    public float exhaust;

    public ArmySlot(Province _province)
    {
        province = _province;
    }

    public void Recruit(Army _army)
    {
        if (!CanRecruit(_army)) return;
        _army.Recruit(province);

        // Instantiate the army
        ArmyManager.InstantiateArmy(province);

        // Update the dynamic panel & stat panel
        UIStatPanel.UpdateCountryStats(province.owner);
        UIDynamicPanel.ShowProvince(province);
    }

    public bool AvailableToRecruit(Army _army)
    {
        // If not the owner
        if (Gameplay.currentCountry.id != province.owner.id) return false;

        // If prequisites are not met
        if (!_army.AvailableToRecruit(province)) return false;

        // If there is already an army 
        if (army != null) return false;

        return true;
    }

    public bool CanRecruit(Army _army)
    {
        // If prequisites are not met
        if (!_army.CanRecruit(province)) return false;

        // If province is not free
        if (province.state != ProvinceState.Free) return false;

        return true;
    }
}