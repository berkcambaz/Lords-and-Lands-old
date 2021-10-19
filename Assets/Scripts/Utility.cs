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

    /// <summary>
    /// Returns the next country, if there is already no country, returns the first country.
    /// </summary>
    /// <param name="_currentCountry"></param>
    /// <returns></returns>
    public static Country GetNextCountry(Country _currentCountry)
    {
        int id = -1;

        // Find the corresponding id if current country exists
        if (_currentCountry != null)
        {
            for (int i = 0; i < World.countries.Length; ++i)
            {
                if (World.countries[i].id == _currentCountry.id)
                {
                    id = (int)_currentCountry.id;
                }
            }
        }

        return World.countries[(id + 1) % World.countryCount];
    }

    public static void ToggleButtonColor(ref Button _button, bool _active)
    {
        ColorBlock colors = _button.colors;

        /// TODO: Sepereate colors as contants
        // Inactive colors are just active color with -20 

        if (_active)
        {
            colors.normalColor = Color.HSVToRGB(0, 0, 100 / 100f);
            colors.highlightedColor = Color.HSVToRGB(0, 0, 96 / 100f);
            colors.pressedColor = Color.HSVToRGB(0, 0, 78 / 100f);
            colors.selectedColor = Color.HSVToRGB(0, 0, 96 / 100f);
            colors.disabledColor = Color.HSVToRGB(0, 0, 78 / 100f);
        }
        else
        {
            colors.normalColor = Color.HSVToRGB(0, 0, 80 / 100f);
            colors.highlightedColor = Color.HSVToRGB(0, 0, 76 / 100f);
            colors.pressedColor = Color.HSVToRGB(0, 0, 58 / 100f);
            colors.selectedColor = Color.HSVToRGB(0, 0, 76 / 100f);
            colors.disabledColor = Color.HSVToRGB(0, 0, 58 / 100f);
        }

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

    public static bool[] GetMoveableProvinces(Province _province)
    {
        // For a province to be moveable, it should have no allied army

        bool[] moveables = new bool[(int)Direction.Count];

        Province top = World.GetProvince(_province.pos + Vector2Int.up);
        Province right = World.GetProvince(_province.pos + Vector2Int.right);
        Province bottom = World.GetProvince(_province.pos + Vector2Int.down);
        Province left = World.GetProvince(_province.pos + Vector2Int.left);

        moveables[(int)Direction.Top] = top != null && top.army == null;
        moveables[(int)Direction.Right] = right != null && right.army == null;
        moveables[(int)Direction.Bottom] = bottom != null && bottom.army == null;
        moveables[(int)Direction.Left] = left != null && left.army == null;

        return moveables;
    }

    public static int GetMoveableProvinceCount(Province _province)
    {
        bool[] moveables = GetMoveableProvinces(_province);
        int moveableCount = 0;

        for (int i = 0; i < moveables.Length; ++i)
        {
            if (moveables[i]) moveableCount++;
        }

        return moveableCount;
    }

    public static float GetProvinceDistance(Province _a, Province _b)
    {
        return (_a.pos - _b.pos).magnitude;
    }
}

public enum Direction
{
    Top,
    Right,
    Bottom,
    Left,
    Count
}