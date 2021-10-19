using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army
{
    public Country country;

    public GameObject gameobject;

    public bool moved;
    public float exhaust;

    public Army(ref Country _country)
    {
        country = _country;

        moved = false;
        exhaust = 0f;
    }

    public float GetOffensive()
    {
        return 0f;
    }

    public float GetDefensive()
    {
        return 0f;
    }
}
