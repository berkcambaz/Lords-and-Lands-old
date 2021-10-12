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

    public Province(ref Country _owner)
    {
        owner = _owner;

        landmark = new Landmark(LandmarkId.None);
    }
}
