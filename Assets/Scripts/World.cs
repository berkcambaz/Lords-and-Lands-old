using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World
{
    private static SeedRandom srandom;

    public static Country[] countries;
    public static Province[] provinces;
    public static int countryCount;
    public static int width = 0;
    public static int height = 0;

    public static void Generate(CountryId[] _countries, int _width, int _height)
    {
        // Clear the tilemap since generating a smaller map after having 
        // generated one would cause the other tiles to be also shown
        ClearTilemap();

        srandom = new SeedRandom();

        countryCount = _countries.Length;
        width = _width;
        height = _height;

        countries = new Country[countryCount];
        provinces = new Province[width * height];

        for (int i = 0; i < countryCount; ++i)
        {
            countries[i] = new Country(_countries[i]);
        }

        List<Vector2Int>[] origins = ChooseOrigins();
        ChooseProvinces(origins);
        SprinkleNature();

        InitTilemap();
        Utility.CenterCamera(false);
    }

    public static Province GetProvince(Vector2Int _pos)
    {
        if (_pos.x >= width || _pos.x < 0 || _pos.y >= height || _pos.y < 0)
            return null;

        return provinces[_pos.x + _pos.y * width];
    }

    private static void InitTilemap()
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Vector2Int pos = new Vector2Int(x, y);
                int index = x + y * width;

                TilemapManager.UpdateProvinceTile(pos, provinces[index]);
            }
        }
    }

    private static void ClearTilemap()
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Vector2Int pos = new Vector2Int(x, y);

                TilemapManager.ClearTile(pos);
            }
        }
    }

    private static List<Vector2Int>[] ChooseOrigins()
    {
        List<Vector2Int>[] origins = new List<Vector2Int>[countryCount];

        for (int i = 0; i < countryCount; ++i)
        {
            origins[i] = new List<Vector2Int>();
            origins[i].Add(new Vector2Int(srandom.Number(0, width), srandom.Number(0, height)));

            for (int j = i - 1; j >= 0; --j)
            {
                if (origins[j][0] == origins[i][0])
                {
                    --i;
                    break;
                }
            }
        }

        // Give the origin provinces to the owner's, otherwise other countries may occupy it
        for (int countryId = 0; countryId < countryCount; ++countryId)
        {
            int originX = origins[countryId][0].x;
            int originY = origins[countryId][0].y;
            int index = originX + originY * width;
            Vector2Int pos = new Vector2Int(originX, originY);
            provinces[index] = new Province(ref countries[countryId], pos);
        }

        return origins;
    }

    private static void ChooseProvinces(List<Vector2Int>[] _origins)
    {
        bool unoccupiedProvincesLeft = true;
        while (unoccupiedProvincesLeft)
        {
            unoccupiedProvincesLeft = false;

            for (int countryId = 0; countryId < countryCount; ++countryId)
            {
                if (_origins[countryId].Count == 0) continue;

                int originX = _origins[countryId][0].x;
                int originY = _origins[countryId][0].y;

                int upIndex = (originX) + (originY - 1) * width;
                int rightIndex = (originX + 1) + (originY) * width;
                int downIndex = (originX) + (originY + 1) * width;
                int leftIndex = (originX - 1) + (originY) * width;

                if (originY - 1 > -1 && provinces[upIndex] == null)
                {
                    Vector2Int pos = new Vector2Int(originX, originY - 1);
                    _origins[countryId].Add(pos);
                    provinces[upIndex] = new Province(ref countries[countryId], pos);
                }
                else if (originX + 1 < width && provinces[rightIndex] == null)
                {
                    Vector2Int pos = new Vector2Int(originX + 1, originY);
                    _origins[countryId].Add(pos);
                    provinces[rightIndex] = new Province(ref countries[countryId], pos);
                }
                else if (originY + 1 < height && provinces[downIndex] == null)
                {
                    Vector2Int pos = new Vector2Int(originX, originY + 1);
                    _origins[countryId].Add(pos);
                    provinces[downIndex] = new Province(ref countries[countryId], pos);
                }
                else if (originX - 1 > -1 && provinces[leftIndex] == null)
                {
                    Vector2Int pos = new Vector2Int(originX - 1, originY);
                    _origins[countryId].Add(pos);
                    provinces[leftIndex] = new Province(ref countries[countryId], pos);
                }
                else
                {
                    _origins[countryId].RemoveAt(0);
                }

                unoccupiedProvincesLeft |= _origins[countryId].Count != 0;
            }
        }
    }

    private static void SprinkleNature()
    {
        for (int i = 0; i < width * height; ++i)
        {
            //provinces[i].landmark.id = (LandmarkId)srandom.Percent(new int[,] {
            //    { 75, (int)LandmarkId.None },
            //    { 15, (int)LandmarkId.Forest },
            //    { 10, (int)LandmarkId.Mountains }
            //});
        }
    }

    public static void Save() { }
    public static void Load() { }
}
