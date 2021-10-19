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

    public GameObject moveableTiles;
    public GameObject tileTop;
    public GameObject tileRight;
    public GameObject tileBottom;
    public GameObject tileLeft;

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

    public static void ShowMoveableTiles(Vector2 _pos, bool[] _moveables)
    {
        Instance.tileTop.SetActive(_moveables[(int)Direction.Top]);
        Instance.tileRight.SetActive(_moveables[(int)Direction.Right]);
        Instance.tileBottom.SetActive(_moveables[(int)Direction.Bottom]);
        Instance.tileLeft.SetActive(_moveables[(int)Direction.Left]);

        Instance.moveableTiles.transform.position = _pos + new Vector2(0.5f, 0.5f);

        Instance.moveableTiles.SetActive(true);
    }

    public static void HideMoveableTiles()
    {
        Instance.moveableTiles.SetActive(false);
    }
}