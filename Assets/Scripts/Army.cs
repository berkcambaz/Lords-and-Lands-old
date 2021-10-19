using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army
{
    public Country country;

    public bool moved;
    public float exhaust;

    public Army(Country _country, bool _moved)
    {
        country = _country;
        moved = _moved;
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
