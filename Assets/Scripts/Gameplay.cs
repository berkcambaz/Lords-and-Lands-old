using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay
{
    public static SeedRandom srandom;

    public static Country currentCountry;
    public static Province currentProvince;

    public static bool started = false;
    public static bool acting = false;

    public static void Start()
    {
        srandom = new SeedRandom();

        started = true;

        NextTurn();
    }

    public static void NextTurn()
    {
        // If country has not placed it's capital yet, don't move to next turn
        if (currentCountry != null && !Utility.GetAlreadyBuilt(currentCountry, BuildingDatabase.GetById(BuildingId.Capital)))
        {
            return;
        }

        if (currentCountry != null)
        {
            // Add the income amount to the gold
            currentCountry.gold += currentCountry.income;

            // Update the armies
            for (int i = 0; i < World.provinces.Length; ++i)
            {
                if (World.provinces[i].armySlot.army != null && World.provinces[i].armySlot.country.id == currentCountry.id)
                {
                    World.provinces[i].armySlot.Update();
                }
            }
        }

        currentCountry = Utility.GetNextCountry(currentCountry);
        currentProvince = null;
        acting = false;

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
        // If the game hasn't started
        if (!started) return;

        if (acting)
        {
            if (_province.armySlot.army != null) currentProvince.armySlot.Attack(_province);
            else currentProvince.armySlot.Move(_province);

            UIManager.HideActionTiles();
            acting = false;
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
}
