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

    public static void InstantiateArmy(ref Province _province)
    {
        Vector3 pos = new Vector3(_province.pos.x + 0.5f, _province.pos.y + Instance.prefabArmy.transform.localScale.y / 2f, 0f);
        _province.ally.gameobject = Instantiate(Instance.prefabArmy, pos, Quaternion.identity, Instance.transform);
    }

    public static void MoveArmy(Province _province)
    {
        Vector3 pos = new Vector3(_province.pos.x + 0.5f, _province.pos.y + Instance.prefabArmy.transform.localScale.y / 2f, 0f);
        _province.ally.gameobject.transform.position = pos;
    }

    public static void DestroyArmy()
    {

    }
}
