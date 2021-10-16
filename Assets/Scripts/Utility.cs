using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utility
{
    public static void CenterCamera(bool _smoothed)
    {
        if (_smoothed)
        {
            /// TODO: Implement
        }
        else
        {
            Camera cam = Game.Instance.cam;
            cam.transform.position = new Vector3(World.width / 2f, World.height / 2f, cam.transform.position.z);
        }
    }

    public static Country GetNextCountry(Country _currentCountry)
    {
        int id = (int)_currentCountry.id;
        return World.countries[(id + 1) % World.countryCount];
    }

    public static void SetButtonColor(ref Button _button, Color _color)
    {
        ColorBlock colors = _button.colors;
        colors.normalColor = _color;
        _button.colors = colors;
    }

    public static string GetProvinceName(Province _province)
    {
        string name = "";

        CountryId ownerId = _province.owner.id;
        CountryId occupierId = _province.occupier == null ? CountryId.None : _province.occupier.id;

        switch (ownerId)
        {
            case CountryId.Green:
                name += "Green's Province";
                break;
            case CountryId.Purple:
                name += "Purple's Province";
                break;
            case CountryId.Red:
                name += "Red's Province";
                break;
            case CountryId.Yellow:
                name += "Yellow's Province";
                break;
            default:
                break;
        }

        switch (occupierId)
        {
            case CountryId.Green:
                name += "(Occupied by Green)";
                break;
            case CountryId.Purple:
                name += "(Occupied by Purple)";
                break;
            case CountryId.Red:
                name += "(Occupied by Red)";
                break;
            case CountryId.Yellow:
                name += "(Occupied by Yellow)";
                break;
            default:
                break;
        }

        return name;
    }
}
