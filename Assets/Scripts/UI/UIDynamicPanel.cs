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
    public Button buttonAct;
    public Button buttonDemolish;

    public Image imageRecruitArmy;
    public Image imageMoveArmy;

    public void Init()
    {
        Instance = this;

        buttonBuildCapital.onClick.AddListener(() =>
        {
            Gameplay.currentProvince.buildingSlot.Build(BuildingDatabase.GetById(BuildingId.Capital));
        });

        buttonBuildForest.onClick.AddListener(() =>
        {
            Gameplay.currentProvince.buildingSlot.Build(BuildingDatabase.GetById(BuildingId.Forest));
        });

        buttonBuildMountains.onClick.AddListener(() =>
        {
            Gameplay.currentProvince.buildingSlot.Build(BuildingDatabase.GetById(BuildingId.Mountains));
        });

        buttonBuildHouse.onClick.AddListener(() =>
        {
            Gameplay.currentProvince.buildingSlot.Build(BuildingDatabase.GetById(BuildingId.House));
        });

        buttonBuildTower.onClick.AddListener(() =>
        {
            Gameplay.currentProvince.buildingSlot.Build(BuildingDatabase.GetById(BuildingId.Tower));
        });

        buttonBuildChurch.onClick.AddListener(() =>
        {
            Gameplay.currentProvince.buildingSlot.Build(BuildingDatabase.GetById(BuildingId.Church));
        });

        buttonRecruit.onClick.AddListener(() =>
        {
            Gameplay.currentProvince.armySlot.Recruit(ArmyDatabase.GetById(ArmyId.Regular));
        });

        buttonAct.onClick.AddListener(() =>
        {
            Gameplay.currentProvince.armySlot.ShowActables();
        });

        buttonDemolish.onClick.AddListener(() =>
        {
            Gameplay.currentProvince.buildingSlot.Demolish();
        });
    }

    public static void ShowProvince(Province _province)
    {
        Instance.textProvinceName.text = Utility.GetProvinceName(_province);

        bool canBuildCapital = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Capital));
        bool canBuildForest = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Forest));
        bool canBuildMountains = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Mountains));
        bool canBuildHouse = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.House));
        bool canBuildTower = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Tower));
        bool canBuildChurch = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Church));
        bool canRecruit = Gameplay.currentProvince.armySlot.CanRecruit(ArmyDatabase.GetById(ArmyId.Regular));
        bool canAct = Gameplay.currentProvince.armySlot.CanAct();
        bool canDemolish = Gameplay.currentProvince.buildingSlot.CanDemolish();

        Utility.ToggleButtonColor(Instance.buttonBuildCapital, canBuildCapital);
        Utility.ToggleButtonColor(Instance.buttonBuildForest, canBuildForest);
        Utility.ToggleButtonColor(Instance.buttonBuildMountains, canBuildMountains);
        Utility.ToggleButtonColor(Instance.buttonBuildHouse, canBuildHouse);
        Utility.ToggleButtonColor(Instance.buttonBuildTower, canBuildTower);
        Utility.ToggleButtonColor(Instance.buttonBuildChurch, canBuildChurch);
        Utility.ToggleButtonColor(Instance.buttonRecruit, canRecruit);
        Utility.ToggleButtonColor(Instance.buttonAct, canAct);
        Utility.ToggleButtonColor(Instance.buttonDemolish, canDemolish);



        bool availableToBuildCapital = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Capital));
        bool availableToBuildForest = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Forest));
        bool availableToBuildMountains = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Mountains));
        bool availableToBuildHouse = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.House));
        bool availableToBuildTower = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Tower));
        bool availableToBuildChurch = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Church));
        bool availableToRecruit = Gameplay.currentProvince.armySlot.AvailableToRecruit(ArmyDatabase.GetById(ArmyId.Regular));
        bool availableToAct = Gameplay.currentProvince.armySlot.AvailableToAct();
        bool availableToDemolish = Gameplay.currentProvince.buildingSlot.AvailableToDemolish();
        
        Instance.buttonBuildCapital.gameObject.SetActive(availableToBuildCapital);
        Instance.buttonBuildForest.gameObject.SetActive(availableToBuildForest);
        Instance.buttonBuildMountains.gameObject.SetActive(availableToBuildMountains);
        Instance.buttonBuildHouse.gameObject.SetActive(availableToBuildHouse);
        Instance.buttonBuildTower.gameObject.SetActive(availableToBuildTower);
        Instance.buttonBuildChurch.gameObject.SetActive(availableToBuildChurch);
        Instance.buttonRecruit.gameObject.SetActive(availableToRecruit);
        Instance.buttonAct.gameObject.SetActive(availableToAct);
        Instance.buttonDemolish.gameObject.SetActive(availableToDemolish);

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
