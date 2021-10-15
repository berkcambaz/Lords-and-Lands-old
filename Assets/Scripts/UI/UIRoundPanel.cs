using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoundPanel 
{
    public static void UpdateNextCountryImage(Country _country)
    {
        UIManager.Instance.imageNextCountry.sprite = AssetManager.GetArmySprite(_country);
    }
}
