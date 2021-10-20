using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province
{
    public Country owner;
    public Country occupier;

    public Landmark landmark;

    public Army army;

    public Vector2Int pos;

    public Province(ref Country _owner, Vector2Int _pos)
    {
        owner = _owner;
        pos = _pos;

        landmark = new Landmark(LandmarkId.None);
    }

    public float GetOffensive()
    {
        // If army is not owner or occupier, gets no bonus
        if (army.country.id != owner.id || army.country.id != occupier.id) return 0f;

        switch (landmark.id)
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
        // If army is not owner or occupier, gets no bonus
        if (army.country.id != owner.id || army.country.id != occupier.id) return 0f;

        switch (landmark.id)
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
        // If army is not owner or occupier, gets no bonus
        if (army.country.id != owner.id || army.country.id != occupier.id) return 0f;

        switch (landmark.id)
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
