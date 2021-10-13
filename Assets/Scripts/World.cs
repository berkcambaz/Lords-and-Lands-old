using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World
{
    private static SeedRandom srandom;

    private static Country[] countries;
    private static Province[] provinces;
    public static int countryCount;
    public static int width;
    public static int height;

    public static void Generate(int _countryCount, int _width, int _height)
    {
        srandom = new SeedRandom();

        countryCount = _countryCount;
        width = _width;
        height = _height;

        countries = new Country[countryCount];
        provinces = new Province[width * height];

        for (int i = 0; i < countryCount; ++i)
        {
            countries[i] = new Country((CountryId)i);
        }

        List<Vector2Int>[] origins = ChooseOrigins();
        ChooseProvinces(origins);
        SprinkleNature();

        InitTilemap();
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
                    _origins[countryId].Add(new Vector2Int(originX, originY - 1));
                    provinces[upIndex] = new Province(ref countries[countryId]);
                }
                else if (originX + 1 < width && provinces[rightIndex] == null)
                {
                    _origins[countryId].Add(new Vector2Int(originX + 1, originY));
                    provinces[rightIndex] = new Province(ref countries[countryId]);
                }
                else if (originY + 1 < height && provinces[downIndex] == null)
                {
                    _origins[countryId].Add(new Vector2Int(originX, originY + 1));
                    provinces[downIndex] = new Province(ref countries[countryId]);
                }
                else if (originX - 1 > -1 && provinces[leftIndex] == null)
                {
                    _origins[countryId].Add(new Vector2Int(originX - 1, originY));
                    provinces[leftIndex] = new Province(ref countries[countryId]);
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
            provinces[i].landmark.id = (LandmarkId)srandom.Percent(new int[,] {
                { 75, (int)LandmarkId.None },
                { 15, (int)LandmarkId.Forest },
                { 10, (int)LandmarkId.Mountains }
            });
        }
    }

    public static void Save() { }
    public static void Load() { }
}
