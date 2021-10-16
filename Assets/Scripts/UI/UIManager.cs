using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject tileHighlight;
    public GameObject tileFocus;

    public UIDynamicPanel uiDynanamicPanel;
    public UIMenu uiMenu;
    public UIRoundPanel uiRoundPanel;
    public UIStatPanel uiStatPanel;

    public void Init()
    {
        Instance = this;

        uiDynanamicPanel.Init();
        uiMenu.Init();
        uiRoundPanel.Init();
        uiStatPanel.Init();
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