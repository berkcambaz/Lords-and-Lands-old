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

    public virtual void Recruit(Province _province)
    {
        _province.owner.gold -= cost;
        _province.owner.army += manpowerCost;

        _province.armySlot.army = this;
        _province.armySlot.gameobject = ArmyManager.InstantiateArmy(_province);
        _province.armySlot.exhaust = 0f;
        _province.armySlot.state = ArmyState.Moved;
    }

    public virtual bool AvailableToRecruit(Province _province)
    {
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
    Moved
}