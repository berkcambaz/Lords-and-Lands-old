using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class BuildingDatabase 
{
    private static Building[] buildings;

    public static void Init()
    {
        // Read all buildings and sort by id
        buildings = Resources.LoadAll<Building>("Scriptable Objects/Buildings");
        Array.Sort(buildings, delegate (Building a, Building b) { return a.id.CompareTo(b.id); });

        // Initialize buildings
        for (int i = 0; i < buildings.Length; ++i)
        {
            buildings[i].Init();
        }
    }

    public static Building GetById(BuildingId _id)
    {
        return buildings[(int)_id];
    }
}
