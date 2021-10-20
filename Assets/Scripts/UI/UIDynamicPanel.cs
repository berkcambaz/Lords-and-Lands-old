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
    public Button buttonBuildMountains;
    public Button buttonBuildHouse;
    public Button buttonBuildTower;
    public Button buttonBuildChurch;
    public Button buttonRecruit;
    public Button buttonMove;
    public Button buttonDestroy;

    public Image imageRecruitArmy;
    public Image imageMoveArmy;

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

        buttonBuildMountains.onClick.AddListener(() =>
        {
            Gameplay.Build(ref Gameplay.currentCountry, ref Gameplay.currentProvince, LandmarkId.Mountains);
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

        buttonRecruit.onClick.AddListener(() =>
        {
            Gameplay.Recruit(ref Gameplay.currentCountry, ref Gameplay.currentProvince);
        });

        buttonMove.onClick.AddListener(() =>
        {
            Gameplay.ShowMoveables(Gameplay.currentCountry, Gameplay.currentProvince);
        });

        buttonDestroy.onClick.AddListener(() =>
        {
            Gameplay.Build(ref Gameplay.currentCountry, ref Gameplay.currentProvince, LandmarkId.None);
        });
    }

    public static void ShowProvince(Province _province)
    {
        Instance.textProvinceName.text = Utility.GetProvinceName(_province);

        bool availableToBuildCapital = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.Capital);
        bool availableToBuildForest = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.Forest);
        bool availableToBuildMountains = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.Mountains);
        bool availableToBuildHouse = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.House);
        bool availableToBuildTower = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.Tower);
        bool availableToBuildChurch = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.Church);
        bool availableToRecruit = Gameplay.AvailableToRecruit(Gameplay.currentCountry, _province);
        bool availableToMove = Gameplay.AvailableToMove(Gameplay.currentCountry, _province);
        bool availableToDestroy = Gameplay.AvailableToBuild(Gameplay.currentCountry, _province, LandmarkId.None); ;

        Instance.buttonBuildCapital.gameObject.SetActive(availableToBuildCapital);
        Instance.buttonBuildForest.gameObject.SetActive(availableToBuildForest);
        Instance.buttonBuildMountains.gameObject.SetActive(availableToBuildMountains);
        Instance.buttonBuildHouse.gameObject.SetActive(availableToBuildHouse);
        Instance.buttonBuildTower.gameObject.SetActive(availableToBuildTower);
        Instance.buttonBuildChurch.gameObject.SetActive(availableToBuildChurch);
        Instance.buttonRecruit.gameObject.SetActive(availableToRecruit);
        Instance.buttonMove.gameObject.SetActive(availableToMove);
        Instance.buttonDestroy.gameObject.SetActive(availableToDestroy);

        Instance.panelDynamic.SetActive(true);
        UIManager.ShowTileFocus(_province.pos);
    }

    public static void HideProvince()
    {
        Instance.panelDynamic.SetActive(false);
        UIManager.HideTileFocus();
    }

    public static void UpdateArmyImage(Country _country)
    {
        Instance.imageMoveArmy.sprite = AssetManager.GetArmySprite(_country);
        Instance.imageRecruitArmy.sprite = AssetManager.GetArmySprite(_country);
    }
}
