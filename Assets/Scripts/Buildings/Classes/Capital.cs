using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Capital", menuName = "Buildings/Capital")]
public class Capital : Building
{
    [Space(20)]
    public int gold;

    public override void OnBuild(ref Country _country)
    {
        base.OnBuild(ref _country);

        _country.gold += gold;
    }
}
