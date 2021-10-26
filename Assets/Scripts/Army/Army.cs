using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : ScriptableObject
{
    public new string name;
    public ArmyId id;

    [Space(10)]
    public int cost;
    public int manpowerCost;

    [Space(10)]
    public float offensive;
    public float defensive;
    public float breakthrough;

    [Space(10)]
    public GameObject prefab;

    public void CreateArmy(Army _army, Country _country, Province _province, float _exhaust, ArmyState _state)
    {
        _province.armySlot.army = _army;
        _province.armySlot.country = _country;
        _province.armySlot.exhaust = _exhaust;
        _province.armySlot.state = _state;

        _province.armySlot.gameobject = ArmyManager.InstantiateArmy(_province);
    }

    public void MoveArmy(Province _provinceFrom, Province _provinceTo)
    {
        _provinceTo.armySlot.gameobject = _provinceFrom.armySlot.gameobject;
        _provinceTo.armySlot.army = _provinceFrom.armySlot.army;
        _provinceTo.armySlot.country = _provinceFrom.armySlot.country;
        _provinceTo.armySlot.exhaust = _provinceFrom.armySlot.exhaust;
        _provinceTo.armySlot.state = ArmyState.Unready;

        _provinceFrom.armySlot.gameobject = null;
        _provinceFrom.armySlot.army = null;
        _provinceFrom.armySlot.country = null;
        _provinceFrom.armySlot.exhaust = 0f;
        _provinceFrom.armySlot.state = ArmyState.Unready;

        ArmyManager.MoveArmy(_provinceTo);
    }

    public virtual void Recruit(Province _province)
    {
        _province.owner.gold -= cost;
        _province.owner.army += manpowerCost;

        CreateArmy(this, _province.owner, _province, 0f, ArmyState.Unready);
    }

    public virtual bool AvailableToRecruit(Province _province)
    {
        // If there is no building, or building is not recruitable
        if (_province.buildingSlot.building == null || !_province.buildingSlot.building.recruitable) return false;

        return true;
    }

    public virtual bool CanRecruit(Province _province)
    {
        // If not enough money
        if (_province.owner.gold < cost) return false;

        // If not enough manpower
        if (_province.owner.army + manpowerCost > _province.owner.manpower) return false;

        return true;
    }

    public virtual void Move(Province _provinceFrom, Province _provinceTo)
    {
        // Transfer the army to the new province and delete old army
        MoveArmy(_provinceFrom, _provinceTo);
    }

    public virtual bool AvailableToMove()
    {
        return true;
    }

    public virtual bool CanMove(Province _provinceFrom, Province _provinceTo)
    {
        // If army is not in ready state
        if (_provinceFrom.armySlot.state != ArmyState.Ready) return false;

        // If distance is not in range
        if (Utility.GetProvinceDistance(_provinceFrom, _provinceTo) != 1f) return false;

        return true;
    }

    public virtual void Attack(Province _provinceFrom, Province _provinceTo)
    {

    }

    public virtual bool AvailableToAttack()
    {
        return true;
    }

    public virtual bool CanAttack(Province _provinceFrom, Province _provinceTo)
    {
        return true;
    }
}

//_province.army.country.army -= 1;
// Update UI if current countries army has died
//if (Gameplay.currentCountry.id == _province.army.country.id)
//    UIStatPanel.UpdateCountryStats(Gameplay.currentCountry);
//_province.army = null;

public enum ArmyId
{
    Regular
}

public enum ArmyState
{
    Ready,
    Unready
}