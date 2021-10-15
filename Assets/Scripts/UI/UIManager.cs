using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject tileHighlight;
    public GameObject tileFocus;

    public Text textGold;
    public Text textIncome;
    public Text textArmy;
    public Text textManpower;

    public Image imageCountry;
    public Image imageNextCountry;

    public GameObject panelDynamic;
    public Text textProvinceName;
    public Button buttonBuildCapital;
    public Button buttonBuildForest;
    public Button buttonBuildHouse;
    public Button buttonBuildTower;
    public Button buttonBuildChurch;

    public void Init()
    {
        Instance = this;
    }

    public static void ShowTileHighlight(Vector2 _pos)
    {
        // Add V(0.5, 0.5) to center it
        Instance.tileHighlight.transform.position = _pos + new Vector2(0.5f, 0.5f);
        Instance.tileHighlight.SetActive(true);
    }

    public static void HideTileHighlight()
    {
        Instance.tileHighlight.SetActive(false);
    }

    public static void ShowTileFocus(Vector2 _pos)
    {
        // Add V(0.5, 0.5) to center it
        Instance.tileFocus.transform.position = _pos + new Vector2(0.5f, 0.5f);
        Instance.tileFocus.SetActive(true);
    }

    public static void HideTileFocus()
    {
        Instance.tileFocus.SetActive(false);
    }
}