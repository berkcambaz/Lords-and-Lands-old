using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army 
{
    public Country country;
    public bool acted;

    public Army (Country _country, bool _acted)
    {
        country = _country;
        acted = _acted;
    }
}
