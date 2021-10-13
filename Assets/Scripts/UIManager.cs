using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject tileHighlight;

    public void Init()
    {
        Instance = this;
    }

    public static void ShowTileHighlight(Vector2 _pos)
    {
        Instance.tileHighlight.transform.position = _pos;
        Instance.tileHighlight.SetActive(true);
    }

    public static void HideTileHighlight()
    {
        Instance.tileHighlight.SetActive(false);
    }
}