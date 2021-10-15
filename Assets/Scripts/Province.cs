using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province 
{
    public Country owner;
    public Country occupier;

    public Landmark landmark;

    public Army ally;
    public Army enemy;

    public Vector2Int pos;

    public Province(ref Country _owner, Vector2Int _pos)
    {
        owner = _owner;
        pos = _pos;

        landmark = new Landmark(LandmarkId.None);
    }
}
