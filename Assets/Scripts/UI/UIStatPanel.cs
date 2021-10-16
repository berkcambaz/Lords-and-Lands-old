using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatPanel : MonoBehaviour
{
    public static UIStatPanel Instance;

    public Image imageCountry;
    public Text textGold;
    public Text textIncome;
    public Text textArmy;
    public Text textManpower;

    public void Init()
    {
        Instance = this;
    }

    public static void UpdateCountryImage(Country _country)
    {
        Instance.imageCountry.sprite = AssetManager.GetArmySprite(_country);
    }

    public static void UpdateCountryStats(Country _country)
    {
        string gold = _country.gold > 999 ? "+999" : _country.gold.ToString();
        string income = _country.income > 999 ? "+999" : _country.income.ToString();
        string army = _country.army > 999 ? "+999" : _country.army.ToString();
        string manpower = _country.manpower > 999 ? "+999" : _country.manpower.ToString();

        Instance.textGold.text = "Gold: " + gold;
        Instance.textIncome.text = "Income: " + income;
        Instance.textArmy.text = "Army: " + army;
        Instance.textManpower.text = "Manpower: " + manpower;
    }
}
