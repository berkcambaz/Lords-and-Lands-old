using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country 
{
    public CountryId id;
    public int gold;
    public int income;
    public int army;
    public int manpower;

    public bool capitalBuilt;

    public Country(CountryId _id = CountryId.None)
    {
        id = _id;
        gold = 0;
        income = 0;
        army = 0;
        manpower = 0;
        capitalBuilt = false;
    }
}

public enum CountryId
{
    None = -1,
    Green,
    Purple,
    Red,
    Yellow,
    Count
}