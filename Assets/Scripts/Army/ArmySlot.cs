using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmySlot
{
    /// <summary>
    /// The province that the army is currently in.
    /// </summary>
    public Province province;

    public Army army;

    /// <summary>
    /// The country that the army belongs to.
    /// </summary>
    public Country country;
    public GameObject gameobject;
    public ArmyState state;
    public float exhaust;

    public ArmySlot(Province _province)
    {
        province = _province;
    }

    public void Update()
    {
        if (army == null) return;

        if (state == ArmyState.Ready) exhaust = Mathf.Clamp(exhaust - 0.25f, 0f, 6f);

        // Attack the land after adding exhaust & before setting state to ready
        army.AttackLand(province);

        state = ArmyState.Ready;
    }

    public void Recruit(Army _army)
    {
        if (!CanRecruit(_army)) return;
        _army.Recruit(province);

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

    public void ShowActables()
    {
        bool[] actables = Utility.GetActableTiles(country, province);
        if (actables.Length == 0) return;

        UIManager.ShowActionTiles(province, actables);
        Gameplay.acting = true;
    }

    public bool AvailableToAct()
    {
        return AvailableToMove() || AvailableToAttack();
    }

    public bool CanAct()
    {
        return army != null && state == ArmyState.Ready && Utility.GetActableTiles(country, province).Length > 0;
    }

    public void Move(Province _province)
    {
        if (!CanMove(_province)) return;
        army.Move(province, _province);

        // Update the dynamic panel & stat panel
        UIStatPanel.UpdateCountryStats(province.owner);
        UIDynamicPanel.ShowProvince(province);
    }

    public bool AvailableToMove()
    {
        // If there is no army
        if (army == null) return false;

        // If not the owner
        if (Gameplay.currentCountry.id != country.id) return false;

        // If prequisites are not met
        if (!army.AvailableToMove()) return false;

        return true;
    }

    public bool CanMove(Province _province)
    {
        // If prequisites are not met
        if (!army.CanMove(province, _province)) return false;

        return true;
    }

    public void Attack(Province _province)
    {
        if (!CanAttack(_province)) return;
        army.Attack(province, _province);

        // Update the dynamaic panel & stat panel
        UIStatPanel.UpdateCountryStats(province.owner);
        UIDynamicPanel.ShowProvince(province);
    }

    public bool AvailableToAttack()
    {
        // If there is no army
        if (army == null) return false;

        // If not the owner
        if (Gameplay.currentCountry.id != country.id) return false;

        // If prequisites are not met
        if (!army.AvailableToAttack()) return false;

        return true;
    }

    public bool CanAttack(Province _province)
    {
        // If prequisites are not met
        if (!army.CanAttack(province, _province)) return false;

        return true;
    }
}