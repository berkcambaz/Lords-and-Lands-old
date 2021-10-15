using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay
{
    public static Country currentCountry;
    public static Province currentProvince;

    public static void Start()
    {
        currentCountry = World.countries[0];

        UIStatPanel.UpdateCountryImage(currentCountry);
        UIStatPanel.UpdateCountryStats(currentCountry);

        UIRoundPanel.UpdateNextCountryImage(Utility.GetNextCountry(currentCountry));
    }

    public static void ChooseProvince(Province _province)
    {
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
