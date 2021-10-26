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

    public void AddEffects(Province _province)
    {
        _province.owner.income += income;
        _province.owner.manpower += manpower;
    }

    public void RemoveEffects(Province _province)
    {
        _province.owner.income -= income;
        _province.owner.manpower -= manpower;
    }

    public virtual void Build(Province _province)
    {
        _province.owner.gold -= cost;
        AddEffects(_province);

        _province.buildingSlot.building = this;
    }

    public virtual bool AvailableToBuild(Province _province)
    {
        // If capital is not built and capital is going to be built, allow it
        bool capitalBuilt = Utility.GetAlreadyBuilt(_province.owner, BuildingDatabase.GetById(BuildingId.Capital));
        if (!capitalBuilt) capitalBuilt = id == BuildingId.Capital;
        
        bool alreadyBuilt = unique && Utility.GetAlreadyBuilt(_province.owner, this);

        return capitalBuilt && !unbuildable && !alreadyBuilt;
    }

    public virtual bool CanBuild(Province _province)
    {
        // If not enough money
        if (_province.owner.gold < cost) return false;

        return true;
    }

    public virtual void Demolish(Province _province)
    {
        RemoveEffects(_province);

        _province.buildingSlot.building = null;
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