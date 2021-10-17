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
            UIManager.HideTileFocus();
        }
        else
        {
            currentProvince = _province;
            UIDynamicPanel.ShowProvince(currentProvince);
            UIManager.ShowTileFocus(_province.pos);
        }
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
                /// TODO: Add build cost to church
                return true;
            case LandmarkId.Forest:
                return true;
            case LandmarkId.House:
                /// TODO: Add build cost to house
                return true;
            case LandmarkId.Mountains:
                return true;
            case LandmarkId.Tower:
                /// TODO: Add build cost to tower
                return true;
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
        if (CanBuild(_country, _province, _landmarkId))
        {
            _province.landmark.id = _landmarkId;
            TilemapManager.UpdateProvinceTile(_province.pos, _province);

            switch (_landmarkId)
            {
                case LandmarkId.None:
                    break;
                case LandmarkId.Capital:
                    _country.gold += 10;
                    _country.income += 1;
                    _country.manpower += 3;
                    _country.capitalBuilt = true;
                    break;
                case LandmarkId.Church:
                    break;
                case LandmarkId.Forest:
                    break;
                case LandmarkId.House:
                    break;
                case LandmarkId.Mountains:
                    break;
                case LandmarkId.Tower:
                    break;
                case LandmarkId.Count:
                    break;
            }

            // Update the dynamic panel & stat panel
            UIStatPanel.UpdateCountryStats(_country);
            UIDynamicPanel.ShowProvince(_province);
        }
    }
}
