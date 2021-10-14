using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject tileHighlight;

    public Text textGold;
    public Text textIncome;
    public Text textArmy;
    public Text textManpower;

    public Image imageCountry;
    public Image imageNextCountry;

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
        Instance.textGold.text = "Gold: " + _country.gold.ToString();
        Instance.textIncome.text = "Income: " + _country.income.ToString();
        Instance.textArmy.text = "Army: " + _country.army.ToString();
        Instance.textManpower.text = "Manpower: " + _country.manpower.ToString();
    }

    public static void ShowTileHighlight(Vector2 _pos)
    {
        Instance.tileHighlight.transform.position = _pos;
        Instance.tileHighlight.SetActive(true);
    }

    public static void HideTileHighlight()
    {
        Instance.tileHighlight.SetActive(false);
    }
}