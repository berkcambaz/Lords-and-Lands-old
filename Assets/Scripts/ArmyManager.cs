using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Instance;

    public void Init()
    {
        Instance = this;
    }

    public static GameObject InstantiateArmy(Province _province)
    {
        GameObject prefab = _province.armySlot.army.prefab;

        prefab.GetComponent<SpriteRenderer>().sprite = AssetManager.GetArmySprite(_province.owner);
        Vector3 pos = new Vector3(_province.pos.x + 0.5f, _province.pos.y + prefab.transform.localScale.y / 2f, 0f);
        GameObject gameobject = Instantiate(prefab, pos, Quaternion.identity, Instance.transform);

        return gameobject;
    }

    public static void MoveArmy(Province _province)
    {
        GameObject gameobject = _province.armySlot.gameobject;

        Vector3 pos = new Vector3(_province.pos.x + 0.5f, _province.pos.y + gameobject.transform.localScale.y / 2f, 0f);
        gameobject.transform.position = pos;
    }

    public static void DestroyArmy(Province _province)
    {
        Destroy(_province.armySlot.gameobject);
    }
}
