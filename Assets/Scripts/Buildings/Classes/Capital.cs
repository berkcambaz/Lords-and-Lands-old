using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Capital", menuName = "Buildings/Capital")]
public class Capital : Building
{
    [Space(20)]
    public int gold;

    public override void Build(ref Country _country)
    {
        base.Build(ref _country);

        _country.gold += gold;
    }
}
