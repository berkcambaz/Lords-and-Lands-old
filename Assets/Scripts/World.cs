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

        ChooseOrigins();
        ChooseProvinces();
        SprinkleNature();
    }

    private static void ChooseOrigins()
    {
        
    }

    private static void ChooseProvinces()
    {

    }

    private static void SprinkleNature()
    {

    }

    public static void Save() { }
    public static void Load() { }
}
