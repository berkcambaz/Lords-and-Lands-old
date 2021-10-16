using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MENU_New : MonoBehaviour
{
    public static MENU_New Instance;

    public GameObject newPanel;
    [Space(10)]
    public Button buttonCountryGreen;
    public Button buttonCountryPurple;
    public Button buttonCountryRed;
    public Button buttonCountryYellow;
    [Space(10)]
    public Text textWidth;
    public Button buttonWidthIncrease;
    public Button buttonWidthDecrease;
    [Space(10)]
    public Text textHeight;
    public Button buttonHeightIncrease;
    public Button buttonHeightDecrease;
    [Space(10)]
    public Button buttonGenerate;
    public Button buttonStart;

    public void Init()
    {
        Instance = this;
    }
}
