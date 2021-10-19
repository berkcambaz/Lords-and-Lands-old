using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark 
{
    public LandmarkId id;

    public Landmark(LandmarkId _id)
    {
        id = _id;
    }
    
    public float GetOffensive()
    {
        switch (id)
        {
            case LandmarkId.None:
                return 0f;
            case LandmarkId.Capital:
                return 0f;
            case LandmarkId.Church:
                return 0f;
            case LandmarkId.Forest:
                return 1f;
            case LandmarkId.House:
                return 0f;
            case LandmarkId.Mountains:
                return 0f;
            case LandmarkId.Tower:
                return 0f;
            default:
                return 0f;
        }
    }

    public float GetDefensive()
    {
        switch (id)
        {
            case LandmarkId.None:
                return 0f;
            case LandmarkId.Capital:
                return 0f;
            case LandmarkId.Church:
                return 0f;
            case LandmarkId.Forest:
                return 0f;
            case LandmarkId.House:
                return 0f;
            case LandmarkId.Mountains:
                return 1f;
            case LandmarkId.Tower:
                return 1f;
            default:
                return 0f;
        }
    }
    
    public float GetDefensiveLand()
    {
        switch (id)
        {
            case LandmarkId.None:
                return 0f;
            case LandmarkId.Capital:
                return 3f;
            case LandmarkId.Church:
                return -1f;
            case LandmarkId.Forest:
                return 1f;
            case LandmarkId.House:
                return 1f;
            case LandmarkId.Mountains:
                return 2f;
            case LandmarkId.Tower:
                return 2f;
            default:
                return 0f;
        }
    }
}

public enum LandmarkId
{
    None = -1,
    Capital,
    Church,
    Forest,
    House,
    Mountains,
    Tower,
    Count
}