using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : ScriptableObject
{
    public new string name;

    [Space(10)]
    public int cost;

    [Space(10)]
    public int income;
    public int manpower;

    [Space(10)]
    public bool unique;
    public bool recruitable;
    public bool unbuildable;

    [Space(10)]
    public float offensive;
    public float defensive;
    public float resistance;

    [Space(10)]
    public Sprite sprite;
    public Color color = Color.white;
    private Tile tile;

    public void Init()
    {
        tile = new Tile();
        tile.sprite = sprite;
        tile.color = color;
    }

    public virtual void OnBuild(ref Country _country)
    {
        _country.gold -= cost;
        _country.income += income;
        _country.manpower += manpower;
    }

    public virtual void OnDemolish(ref Country _country)
    {
        _country.income -= income;
        _country.manpower -= manpower;
    }

    public virtual void OnFree()
    {

    }

    public virtual void OnAttack()
    {

    }

    public virtual void OnOccupy()
    {

    }

    public virtual void OnSurrender()
    {

    }
}
