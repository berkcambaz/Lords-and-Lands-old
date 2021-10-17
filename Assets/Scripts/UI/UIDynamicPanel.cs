using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDynamicPanel : MonoBehaviour
{
    public static UIDynamicPanel Instance;

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

        buttonBuildCapital.onClick.AddListener(() =>
        {
            Gameplay.Build(ref Gameplay.currentCountry, ref Gameplay.currentProvince, LandmarkId.Capital);
        });

        buttonBuildForest.onClick.AddListener(() =>
        {
            Gameplay.Build(ref Gameplay.currentCountry, ref Gameplay.currentProvince, LandmarkId.Forest);
        });

        buttonBuildHouse.onClick.AddListener(() =>
        {
            Gameplay.Build(ref Gameplay.currentCountry, ref Gameplay.currentProvince, LandmarkId.House);
        });

        buttonBuildTower.onClick.AddListener(() =>
        {
            Gameplay.Build(ref Gameplay.currentCountry, ref Gameplay.currentProvince, LandmarkId.Tower);
        });

        buttonBuildChurch.onClick.AddListener(() =>
        {
            Gameplay.Build(ref Gameplay.currentCountry, ref Gameplay.currentProvince, LandmarkId.Church);
        });
    }

    public static void ShowProvince(Province _province)
    {
        Instance.textProvinceName.text = Utility.GetProvinceName(_province);

        bool availableToBuildCapital = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.Capital);
        bool availableToBuildForest = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.Forest);
        bool availableToBuildHouse = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.House);
        bool availableToBuildTower = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.Tower);
        bool availableToBuildChurch = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.Church);

        Instance.buttonBuildCapital.gameObject.SetActive(availableToBuildCapital);
        Instance.buttonBuildForest.gameObject.SetActive(availableToBuildForest);
        Instance.buttonBuildHouse.gameObject.SetActive(availableToBuildHouse);
        Instance.buttonBuildTower.gameObject.SetActive(availableToBuildTower);
        Instance.buttonBuildChurch.gameObject.SetActive(availableToBuildChurch);

        Instance.panelDynamic.SetActive(true);
        UIManager.ShowTileFocus(_province.pos);
    }

    public static void HideProvince()
    {
        Instance.panelDynamic.SetActive(false);
        UIManager.HideTileFocus();
    }
}
