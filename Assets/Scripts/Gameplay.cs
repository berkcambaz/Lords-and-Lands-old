using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay
{
    private static SeedRandom srandom;

    public static Country currentCountry;
    public static Province currentProvince;

    public static bool choosingArmyMovement = false;

    public static void Start()
    {
        srandom = new SeedRandom();

        NextTurn();
    }

    public static void NextTurn()
    {
        // If country has not placed it's capital yet, don't move to next turn
        if (currentCountry != null && !currentCountry.capitalBuilt) return;

        // Army movement is not going to be chosen anymore, since turn is now other player's
        choosingArmyMovement = false;
        UIManager.HideMoveableTiles();

        // Add the income amount to the gold
        currentCountry.gold += currentCountry.income;

        // Update the armies
        for (int i = 0; i < World.provinces.Length; ++i)
        {
            if (World.provinces[i].army != null && World.provinces[i].army.country.id == currentCountry.id)
            {
                World.provinces[i].army.Update(ref currentProvince);
            }
        }

        currentCountry = Utility.GetNextCountry(currentCountry);
        currentProvince = null;

        // Update UI
        UIDynamicPanel.UpdateArmyImage(currentCountry);
        UIStatPanel.UpdateCountryImage(currentCountry);
        UIStatPanel.UpdateCountryStats(currentCountry);
        UIRoundPanel.UpdateNextCountryImage(Utility.GetNextCountry(currentCountry));

        // Hide the dynamic panel to avoid bug where another nation can build to another
        // nation if has selected their's province before clicking next turn
        UIDynamicPanel.HideProvince();
    }

    public static void ChooseProvince(Province _province)
    {
        if (choosingArmyMovement)
        {
            Move(ref currentCountry, ref currentProvince, ref _province);
        }

        if (currentProvince == _province || _province == null)
        {
            currentProvince = null;
            UIDynamicPanel.HideProvince();
        }
        else
        {
            currentProvince = _province;
            UIDynamicPanel.ShowProvince(currentProvince);
        }
    }

    public static void ShowMoveables(Country _country, Province _province)
    {
        bool[] moveables = Utility.GetMoveableProvinces(_country, _province);
        UIManager.ShowMoveableTiles(_province.pos, moveables);
        choosingArmyMovement = true;
    }

    public static bool AvailableToMove(Country _country, Province _province)
    {
        // If there is no army or it's not ours
        if (_province.army == null || _province.army.country.id != _country.id) return false;

        // If there is less than 1 moveable province bordering
        if (Utility.GetMoveableProvinceCount(_country, _province) < 1) return false;

        // If army has alread moved
        if (_province.army.moved) return false;

        return true;
    }

    public static void Move(ref Country _country, ref Province _provinceFrom, ref Province _provinceTo)
    {
        choosingArmyMovement = false;
        UIManager.HideMoveableTiles();

        // If army movement range is smaller than province distance, don't move
        // TODO: 1f is current movement range for armies, which is only 1 province
        // to the right, left, up or down. Make it obtained from army class.
        if (Utility.GetProvinceDistance(_provinceFrom, _provinceTo) != 1f) return;

        // If there is a ally army in the target province, don't move
        if (_provinceTo.army != null && _provinceTo.army.country.id == _country.id) return;

        // If there is a enemy army in the target province, attack them
        if (_provinceTo.army != null && _provinceTo.army.country.id != _country.id)
        {
            Attack(ref _provinceFrom, ref _provinceTo);
            return;
        }

        // Move the army to the target province
        _provinceTo.army = _provinceFrom.army;
        _provinceFrom.army = null;
        _provinceTo.army.moved = true;
        ArmyManager.MoveArmy(_provinceTo);
    }

    public static void Attack(ref Province _provinceFrom, ref Province _provinceTo)
    {
        // Dice 0 to 6
        float allyDice = srandom.Dice();
        float enemyDice = srandom.Dice();

        // Add offensive bonus to attacker(ally), add defensive bonus to defender(enemy)
        allyDice += _provinceFrom.landmark.GetOffensive();
        enemyDice += _provinceTo.landmark.GetDefensive();

        // Add support bonuses
        allyDice += Utility.GetSupportBonus(_provinceFrom, _provinceTo.army.country);
        enemyDice += Utility.GetSupportBonus(_provinceTo, _provinceFrom.army.country);

        // Add army bonuses
        allyDice += _provinceFrom.army.GetOffensive();
        enemyDice += _provinceTo.army.GetDefensive();

        // Add exhaust modifier to defender(enemy)
        //allyDice -= _provinceFrom.army.exhaust;
        enemyDice -= _provinceTo.army.exhaust;

        if (allyDice > enemyDice)
        {
            // Destroy enemy army
            ArmyManager.DestroyArmy(ref _provinceTo);

            // Move the army to the target province
            _provinceTo.army = _provinceFrom.army;
            _provinceFrom.army = null;
            _provinceTo.army.moved = true;
            ArmyManager.MoveArmy(_provinceTo);
        }
        else if (allyDice < enemyDice)
        {
            // Destroy allied army
            ArmyManager.DestroyArmy(ref _provinceFrom);

            // Add exhaust to defender(enemy)
            _provinceTo.army.exhaust += 0.25f;

            // Mark defender as attacked
            _provinceTo.army.attacked = true;
        }
        else if (allyDice == enemyDice)
        {
            // Add exhaust to defender(enemy)
            _provinceTo.army.exhaust += 0.5f;

            // Mark defender as attacked
            _provinceTo.army.attacked = true;

            // Mark attacker(ally) as moved
            _provinceFrom.army.moved = true;
        }
    }

    public static void AttackLand(ref Province _province)
    {
        // Army can't attack own lands unless it's occupied
        if (_province.owner.id == _province.army.country.id && _province.occupier == null) return;
    }

    public static bool CanRecruit(Country _country, Province _province)
    {
        // If money is not enough
        if (_country.gold < Constants.CostArmy) return false;

        // If manpower is not enough 
        if (_country.manpower < _country.army + 1) return false;

        return true;
    }

    public static bool AvailableToRecruit(Country _country, Province _province)
    {
        // Check if it's the province owner
        if (_country.id != _province.owner.id) return false;

        // If province is occupied
        if (_province.occupier != null) return false;

        // If there is a army in the province
        if (_province.army != null) return false;

        // Only allow recruiting armies in the house provinces
        switch (_province.landmark.id)
        {
            case LandmarkId.House:
                return true;
        }

        return false;
    }

    public static void Recruit(ref Country _country, ref Province _province)
    {
        if (!CanRecruit(_country, _province)) return;

        _province.army = new Army(ref _country);

        _country.gold -= Constants.CostArmy;
        _country.army += 1;

        // Instantiate the army
        ArmyManager.InstantiateArmy(ref _province);

        // Update the dynamic panel & stat panel
        UIStatPanel.UpdateCountryStats(_country);
        UIDynamicPanel.ShowProvince(_province);
    }

    public static bool CanBuild(Country _country, Province _province, LandmarkId _landmarkId)
    {
        switch (_landmarkId)
        {
            case LandmarkId.None:
                return false;
            case LandmarkId.Capital:
                return true;
            case LandmarkId.Church:
                return _country.gold >= Constants.CostChurch;
            case LandmarkId.Forest:
                return false;
            case LandmarkId.House:
                return _country.gold >= Constants.CostHouse;
            case LandmarkId.Mountains:
                return false;
            case LandmarkId.Tower:
                return _country.gold >= Constants.CostTower;
        }

        return false;
    }

    public static bool AvailableToBuild(Country _country, Province _province, LandmarkId _landmarkId)
    {
        // Check if it's the province owner
        if (_country.id != _province.owner.id) return false;

        // If province is occupied
        if (_province.occupier != null) return false;

        // If there is a landmark already and you are not destroying it
        if (_province.landmark.id != LandmarkId.None && _landmarkId != LandmarkId.None) return false;

        switch (_landmarkId)
        {
            case LandmarkId.None:
                // If it's not destroying the capital & a capital is built, allow it
                return _province.landmark.id != LandmarkId.Capital && _country.capitalBuilt;
            case LandmarkId.Capital:
                return !_country.capitalBuilt;
            case LandmarkId.Church:
                return _country.capitalBuilt;
            case LandmarkId.Forest:
                return false;
            case LandmarkId.House:
                return _country.capitalBuilt;
            case LandmarkId.Mountains:
                return false;
            case LandmarkId.Tower:
                return _country.capitalBuilt;
        }

        return false;
    }

    public static void Build(ref Country _country, ref Province _province, LandmarkId _landmarkId)
    {
        /// TODO: Handle destroying the current landmark

        if (!CanBuild(_country, _province, _landmarkId)) return;

        _province.landmark.id = _landmarkId;
        TilemapManager.UpdateProvinceTile(_province.pos, _province);

        switch (_landmarkId)
        {
            case LandmarkId.None:
                break;
            case LandmarkId.Capital:
                _country.gold += Constants.CapitalGold;
                _country.income += Constants.CapitalIncome;
                _country.manpower += Constants.CapitalManpower;
                _country.capitalBuilt = true;
                break;
            case LandmarkId.Church:
                _country.income += Constants.ChurchIncome;
                _country.gold -= Constants.CostChurch;
                break;
            case LandmarkId.Forest:
                break;
            case LandmarkId.House:
                _country.manpower += Constants.HouseManpower;
                _country.gold -= Constants.CostHouse;
                break;
            case LandmarkId.Mountains:
                break;
            case LandmarkId.Tower:
                _country.gold -= Constants.CostTower;
                break;
        }

        // Update the dynamic panel & stat panel
        UIStatPanel.UpdateCountryStats(_country);
        UIDynamicPanel.ShowProvince(_province);
    }
}
