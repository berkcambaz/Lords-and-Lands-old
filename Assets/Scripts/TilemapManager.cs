using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public static TilemapManager Instance;

    public Tilemap tilemapCountry;
    public Tilemap tilemapLandmark;

    public TileBase[] countryTiles;
    public TileBase[] countryOccupiedTiles;
    public TileBase[] landmarkTiles;

    public void Init()
    {
        Instance = this;
    }

    public static void SetCountryTile(Vector2Int _pos, Country _country, bool _occupied)
    {
        if (_country == null || _country.id == CountryId.None) return;

        int id = (int)_country.id;

        if (_occupied) Instance.tilemapCountry.SetTile((Vector3Int)_pos, Instance.countryOccupiedTiles[id]);
        else Instance.tilemapCountry.SetTile((Vector3Int)_pos, Instance.countryTiles[id]);
    }

    public static void SetLandmarkTile(Vector2Int _pos, Landmark _landmark)
    {
        if (_landmark == null || _landmark.id == LandmarkId.None)
        {
            Instance.tilemapLandmark.SetTile((Vector3Int)_pos, null);
        }
        else
        {
            int id = (int)_landmark.id;
            Instance.tilemapLandmark.SetTile((Vector3Int)_pos, Instance.landmarkTiles[id]);
        }
    }
}
