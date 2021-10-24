using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : ScriptableObject
{
    public new string name;
    public BuildingId id;

    [Space(10)]
    public int cost;

    [Space(10)]
    public int income;
    public int manpower;

    [Space(10)]
    public bool unique;
    public bool recruitable;
    public bool unbuildable;
    public bool undestroyable;

    [Space(10)]
    public float offensive;
    public float defensive;
    public float resistance;

    [Space(10)]
    public Sprite sprite;
    public Color color = Color.white;
    [System.NonSerialized] public Tile tile;

    public virtual void Init()
    {
        tile = CreateInstance<Tile>();
        tile.sprite = sprite;
        tile.color = color;
    }

    public virtual void Build(Province _province)
    {
        _province.owner.gold -= cost;
        _province.owner.income += income;
        _province.owner.manpower += manpower;

        _province.building = this;
    }

    public virtual bool AvailableToBuild(Country _country)
    {
        bool alreadyBuilt = unique && Utility.GetAlreadyBuilt(_country, this);
        return !unbuildable && !alreadyBuilt;
    }

    public virtual bool CanBuild(Country _country)
    {
        return _country.gold >= cost;
    }

    public virtual void Demolish(Province _province)
    {
        _province.owner.income -= income;
        _province.owner.manpower -= manpower;

        _province.building = null;
    }

    public virtual bool AvailableToDemolish()
    {
        return !undestroyable;
    }

    public virtual bool CanDemolish()
    {
        return true;
    }
}

public enum BuildingId
{
    Capital,
    Church,
    Forest,
    House,
    Mountains,
    Tower
}