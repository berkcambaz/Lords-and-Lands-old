using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatPanel 
{
    public static void UpdateCountryImage(Country _country)
    {
        UIManager.Instance.imageCountry.sprite = AssetManager.GetArmySprite(_country);
    }

    public static void UpdateCountryStats(Country _country)
    {
        string gold = _country.gold > 999 ? "+999" : _country.gold.ToString();
        string income = _country.income > 999 ? "+999" : _country.income.ToString();
        string army = _country.army > 999 ? "+999" : _country.army.ToString();
        string manpower = _country.manpower > 999 ? "+999" : _country.manpower.ToString();

        UIManager.Instance.textGold.text = "Gold: " + gold;
        UIManager.Instance.textIncome.text = "Income: " + income;
        UIManager.Instance.textArmy.text = "Army: " + army;
        UIManager.Instance.textManpower.text = "Manpower: " + manpower;
    }
}
