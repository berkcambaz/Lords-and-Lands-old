using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Instance;

    public GameObject prefabArmy;

    public void Init()
    {
        Instance = this;
    }
}
