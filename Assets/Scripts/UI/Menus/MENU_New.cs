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

    // State
    private bool countryGreen = true;
    private bool countryPurple = true;
    private bool countryRed = true;
    private bool countryYellow = true;
    private int width = 10;
    private int height = 10;

    public void Init()
    {
        Instance = this;

        buttonCountryGreen.onClick.AddListener(ToggleCountryGreen);
        buttonCountryPurple.onClick.AddListener(ToggleCountryPurple);
        buttonCountryRed.onClick.AddListener(ToggleCountryRed);
        buttonCountryYellow.onClick.AddListener(ToggleCountryYellow);

        buttonWidthIncrease.onClick.AddListener(IncreaseWidth);
        buttonWidthDecrease.onClick.AddListener(DecreaseWidth);

        buttonHeightIncrease.onClick.AddListener(IncreaseHeight);
        buttonHeightDecrease.onClick.AddListener(DecreaseHeight);
    }

    public static void ToggleCountryGreen()
    {
        Instance.countryGreen = !Instance.countryGreen;
        Utility.SetButtonColor(ref Instance.buttonCountryGreen, Instance.countryGreen ? Color.white : Color.gray);
    }

    public static void ToggleCountryPurple()
    {
        Instance.countryPurple = !Instance.countryPurple;
        Utility.SetButtonColor(ref Instance.buttonCountryPurple, Instance.countryPurple ? Color.white : Color.gray);
    }

    public static void ToggleCountryRed()
    {
        Instance.countryRed = !Instance.countryRed;
        Utility.SetButtonColor(ref Instance.buttonCountryRed, Instance.countryRed ? Color.white : Color.gray);
    }

    public static void ToggleCountryYellow()
    {
        Instance.countryYellow = !Instance.countryYellow;
        Utility.SetButtonColor(ref Instance.buttonCountryYellow, Instance.countryYellow ? Color.white : Color.gray);
    }

    public static void IncreaseWidth()
    {
        if (Instance.width < 25) Instance.width++;    /// TODO: Make 25 a constant
        Instance.textWidth.text = Instance.width.ToString();
    }

    public static void DecreaseWidth()
    {
        if (Instance.width > 10) Instance.width--;    /// TODO: Make 10 a constant
        Instance.textWidth.text = Instance.width.ToString();
    }

    public static void IncreaseHeight()
    {
        if (Instance.height < 25) Instance.height++;    /// TODO: Make 25 a constant
        Instance.textHeight.text = Instance.height.ToString();
    }

    public static void DecreaseHeight()
    {
        if (Instance.height > 10) Instance.height--;    /// TODO: Make 10 a constant
        Instance.textHeight.text = Instance.height.ToString();
    }
}
