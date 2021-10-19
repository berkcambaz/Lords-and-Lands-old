using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army
{
    public Country country;

    public GameObject gameobject;

    public bool moved;
    public bool attacked;
    public float exhaust;

    public Army(ref Country _country)
    {
        country = _country;

        moved = false;
        attacked = false;
        exhaust = 0f;
    }

    public void Update(ref Province _province)
    {
        if (!moved && !attacked)
        {
            exhaust = Mathf.Clamp(exhaust - 0.5f, 0f, 6f);
        }

        if (!moved)
        {
            Gameplay.AttackLand(ref _province);
        }

        moved = false;
        attacked = false;
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
