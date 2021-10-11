using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark 
{
    public LandmarkId id;
}

public enum LandmarkId
{
    None = -1,
    Capital,
    Church,
    Tower,
    Mountains,
    Forest,
    House,
    Count
}