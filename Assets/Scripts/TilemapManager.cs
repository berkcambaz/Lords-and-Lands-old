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

    public static void UpdateProvinceTile(Province _province)
    {
        int id;
        Vector3Int pos = (Vector3Int)_province.pos;

        if (_province.occupier != null)
        {
            id = (int)_province.occupier.id;
            Instance.tilemapProvince.SetTile(pos, Instance.provinceOccupiedTiles[id]);
        }
        else
        {
            id = (int)_province.owner.id;
            Instance.tilemapProvince.SetTile(pos, Instance.provinceTiles[id]);
        }

        if (_province.buildingSlot.building == null)
        {
            Instance.tilemapLandmark.SetTile(pos, null);
        }
        else
        {
            Instance.tilemapLandmark.SetTile(pos, _province.buildingSlot.building.tile);
        }
    }

    public static void ClearTile(Vector2Int _pos)
    {
        Instance.tilemapProvince.SetTile((Vector3Int)_pos, null);
        Instance.tilemapLandmark.SetTile((Vector3Int)_pos, null);
    }
}
