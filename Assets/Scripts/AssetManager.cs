using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    public Sprite[] landmarkSprites;
    public Sprite[] armySprites;

    public void Init()
    {
        Instance = this;
    }

    public static Sprite GetLandmarkSprite(/*Landmark _landmark*/)
    {
        /*
        switch (_landmark.id)
        {
            case LandmarkId.None:
                return null;
            case LandmarkId.Capital:
            case LandmarkId.Church:
            case LandmarkId.Forest:
            case LandmarkId.House:
            case LandmarkId.Mountains:
            case LandmarkId.Tower:
                int id = (int)_landmark.id;
                return Instance.landmarkSprites[id];
            default:
                return null;
        }
        */

        return null;
    }

    public static Sprite GetArmySprite(Country _country)
    {
        switch (_country.id)
        {
            case CountryId.None:
                return null;
            case CountryId.Green:
            case CountryId.Purple:
            case CountryId.Red:
            case CountryId.Yellow:
                int id = (int)_country.id;
                return Instance.armySprites[id];
            default:
                return null;
        }
    }
}
