using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country 
{
    public CountyId id;
    public int gold;
    public int income;
    public int army;
    public int manpower;
}

public enum CountyId
{
    None = -1,
    Green,
    Purple,
    Red,
    Yellow,
    Count
}