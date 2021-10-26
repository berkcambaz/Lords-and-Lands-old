using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Canvas canvasMenu;
    public Canvas canvasHUD;

    public GameObject tileHighlight;
    public GameObject tileFocus;

    public GameObject actionTileContainer;
    public GameObject actionTileTop;
    public GameObject actionTileRight;
    public GameObject actionTileBottom;
    public GameObject actionTileLeft;

    public UIMenu uiMenu;
    public UIDynamicPanel uiDynanamicPanel;
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

    public static void ShowMenu()
    {
        Instance.canvasMenu.gameObject.SetActive(true);
    }

    public static void HideMenu()
    {
        Instance.canvasMenu.gameObject.SetActive(false);
    }

    public static void ShowHUD()
    {
        Instance.canvasHUD.gameObject.SetActive(true);
    }

    public static void HideHUD()
    {
        Instance.canvasHUD.gameObject.SetActive(false);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_province"></param>
    /// <returns>Number of actable tiles.</returns>
    public static void ShowActionTiles(Province _province, bool[] _actables)
    {
        Vector2Int pos = _province.pos;

        Instance.actionTileTop.SetActive(_actables[(int)Direction.Top]);
        Instance.actionTileRight.SetActive(_actables[(int)Direction.Right]);
        Instance.actionTileBottom.SetActive(_actables[(int)Direction.Bottom]);
        Instance.actionTileLeft.SetActive(_actables[(int)Direction.Left]);

        Instance.actionTileContainer.transform.position = pos + new Vector2(0.5f, 0.5f);

        Instance.actionTileContainer.SetActive(true);
    }

    public static void HideActionTiles()
    {
        Instance.actionTileTop.SetActive(false);
        Instance.actionTileRight.SetActive(false);
        Instance.actionTileBottom.SetActive(false);
        Instance.actionTileLeft.SetActive(false);

        Instance.actionTileContainer.SetActive(false);
    }
}