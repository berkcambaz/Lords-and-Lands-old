using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public static TilemapManager Instance;

    public Tilemap tilemapProvince;
    public Tilemap tilemapLandmark;

    public TileBase[] provinceTiles;
    public TileBase[] provinceOccupiedTiles;

    public void Init()
    {
        Instance = this;
    }

    public static void UpdateProvinceTile(Vector2Int _pos, Province _province)
    {
        int id;

        if (_province.occupier != null)
        {
            id = (int)_province.occupier.id;
            Instance.tilemapProvince.SetTile((Vector3Int)_pos, Instance.provinceOccupiedTiles[id]);
        }
        else
        {
            id = (int)_province.owner.id;
            Instance.tilemapProvince.SetTile((Vector3Int)_pos, Instance.provinceTiles[id]);
        }

        if (_province.building == null)
        {
            Instance.tilemapLandmark.SetTile((Vector3Int)_pos, null);
        }
        else
        {
            Instance.tilemapLandmark.SetTile((Vector3Int)_pos, _province.building.tile);
        }
    }

    public static void ClearTile(Vector2Int _pos)
    {
        Instance.tilemapProvince.SetTile((Vector3Int)_pos, null);
        Instance.tilemapLandmark.SetTile((Vector3Int)_pos, null);
    }
}
