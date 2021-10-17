using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoundPanel : MonoBehaviour
{
    public static UIRoundPanel Instance;

    public Image imageNextCountry;

    public Button buttonNextTurn;
    public Button buttonUndo;
    public Button buttonRedo;
    
    public void Init()
    {
        Instance = this;

        buttonNextTurn.onClick.AddListener(Gameplay.NextTurn);
    }

    public static void UpdateNextCountryImage(Country _country)
    {
        Instance.imageNextCountry.sprite = AssetManager.GetArmySprite(_country);
    }
}
