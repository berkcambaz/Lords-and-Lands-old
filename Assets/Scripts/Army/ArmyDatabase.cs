using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArmyDatabase
{
    private static Army[] armies;

    public static void Init()
    {
        // Read all armies and sort by id
        armies = Resources.LoadAll<Army>("Scriptable Objects/Armies");
        Array.Sort(armies, delegate (Army a, Army b) { return a.id.CompareTo(b.id); });
    }

    public static Army GetById(ArmyId _id)
    {
        return armies[(int)_id];
    }
}
