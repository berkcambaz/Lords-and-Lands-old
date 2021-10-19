using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    public static UIMenu Instance;

    public GameObject mainPanel;
    public Button buttonNew;
    public Button buttonSave;
    public Button buttonLoad;
    public Button buttonSettings;
    public Button buttonCredits;
    public Button buttonQuit;

    public MENU_New menuNew;

    private GameObject currentSubmenu;

    public void Init()
    {
        Instance = this;

        // Add listeners to buttons
        buttonNew.onClick.AddListener(ShowNewPanel);
        buttonQuit.onClick.AddListener(Game.Quit);

        // Initialize menus
        menuNew.Init();
    }

    public static void ShowNewPanel()
    {
        HideCurrentSubmenu();
        NewSubmenu(ref MENU_New.Instance.newPanel);
    }

    public static void HideNewPanel()
    {
        HideCurrentSubmenu();
    }

    public static void IncreaseWidth()
    {

    }

    /// <summary>
    /// Sets the new submenu and activates it.
    /// </summary>
    /// <param name="_submenu"></param>
    private static void NewSubmenu(ref GameObject _submenu)
    {
        Instance.currentSubmenu = _submenu;
        Instance.currentSubmenu.SetActive(true);
    }

    /// <summary>
    /// Hides the current submenu.
    /// </summary>
    public static void HideCurrentSubmenu()
    {
        if (Instance.currentSubmenu != null)
            Instance.currentSubmenu.SetActive(false);
    }
}
