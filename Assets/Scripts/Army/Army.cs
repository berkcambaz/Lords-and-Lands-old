using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : ScriptableObject
{
    public new string name;
    public ArmyId id;

    [Space(10)]
    public int cost;

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

        // Increase the army size by 1
        _province.owner.army += 1;
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

    public void DestroyArmy(Province _province)
    {
        ArmyManager.DestroyArmy(_province);

        // Decrease the army size by 1
        _province.armySlot.country.army -= 1;

        _province.armySlot.gameobject = null;
        _province.armySlot.army = null;
        _province.armySlot.country = null;
        _province.armySlot.exhaust = 0f;
        _province.armySlot.state = ArmyState.Unready;
    }

    public virtual void Recruit(Province _province)
    {
        _province.owner.gold -= cost;

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
        if (_province.owner.army + 1 > _province.owner.manpower) return false;

        return true;
    }

    public virtual void Move(Province _provinceFrom, Province _provinceTo)
    {
        // Transfer the army to the new province and delete old army
        MoveArmy(_provinceFrom, _provinceTo);

        // If the old province was attack by our army, now it's not
        if (_provinceFrom.state == ProvinceState.Attacked) _provinceFrom.state = ProvinceState.Free;

        // If the new province is not attacked, it's now attacked by our army
        if (_provinceTo.state == ProvinceState.Free) _provinceTo.state = ProvinceState.Attacked;
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
        float allyDice = Gameplay.srandom.Dice();
        float enemyDice = Gameplay.srandom.Dice();

        // Add offensive for ally & defensive for enemy
        allyDice += _provinceFrom.armySlot.army.offensive;
        enemyDice += _provinceTo.armySlot.army.defensive;

        // Add land bonuses
        allyDice += _provinceFrom.buildingSlot.GetOffensive(_provinceFrom.armySlot.country);
        enemyDice += _provinceTo.buildingSlot.GetDefensive(_provinceTo.armySlot.country);

        // Add support bonuses
        allyDice += Utility.GetSupportBonus(_provinceFrom, _provinceTo.armySlot.country);
        enemyDice += Utility.GetSupportBonus(_provinceTo, _provinceFrom.armySlot.country);

        // Subtract exhaust modifier from enemy
        enemyDice -= _provinceTo.armySlot.exhaust;

        if (allyDice > enemyDice)
        {
            DestroyArmy(_provinceTo);
            MoveArmy(_provinceFrom, _provinceTo);
        }
        else if (allyDice == enemyDice)
        {
            // Add exhaust to enemy
            _provinceTo.armySlot.exhaust += 0.75f;

            // Set state of ally to unready
            _provinceFrom.armySlot.state = ArmyState.Unready;
        }
        else if (allyDice < enemyDice)
        {
            // Destroy ally army
            DestroyArmy(_provinceFrom);

            // Add exhaust to enemy
            _provinceTo.armySlot.exhaust += 0.5f;
        }
    }

    public virtual bool AvailableToAttack()
    {
        return true;
    }

    public virtual bool CanAttack(Province _provinceFrom, Province _provinceTo)
    {
        // If army is not in ready state
        if (_provinceFrom.armySlot.state != ArmyState.Ready) return false;

        // If distance is not in range
        if (Utility.GetProvinceDistance(_provinceFrom, _provinceTo) != 1f) return false;

        return true;
    }

    public virtual void AttackLand(Province _province)
    {
        // If you own the province and it's free
        if (_province.owner.id == _province.armySlot.country.id && _province.state == ProvinceState.Free) return;

        // If army is not ready
        if (_province.armySlot.state != ArmyState.Ready) return;

        float allyDice = Gameplay.srandom.Dice();
        float enemyDice = Gameplay.srandom.Dice();

        // Add breakthrough to ally & resistance to enemy
        allyDice += _province.armySlot.army.breakthrough;
        enemyDice += _province.buildingSlot.GetResistance();

        // Subtract exhaust from the ally
        allyDice -= _province.armySlot.exhaust;

        // Add support bonus to ally
        allyDice += Utility.GetSupportBonus(_province, _province.owner);


        if (allyDice > enemyDice)
        {
            // If unsieging own land
            if (_province.owner.id == _province.armySlot.country.id)
            {
                _province.Unsiege();
            }
            // If sieging unsieged land
            else if (_province.occupier == null)
            {
                _province.Siege(_province.armySlot);
            }
            // If sieging land sieged by somebody else
            else if (_province.occupier.id != _province.armySlot.country.id)
            {
                _province.Siege(_province.armySlot);
            }
            // If conquering sieged
            else
            {
                _province.Conquer(_province.armySlot);
            }
        }

        TilemapManager.UpdateProvinceTile(_province);
    }
}

//_province.army.country.army -= 1;
// Update UI if current countries army has died
//if (Gameplay.currentCountry.id == _province.army.country.id)
//    UIStatPanel.UpdateCountryStats(Gameplay.currentCountry);
//_province.army = null;

public enum ArmyId
{
    Regular,
    Swordman,
    Spearman,
    Archer,
    Knight
}

public enum ArmyState
{
    Ready,
    Unready
}