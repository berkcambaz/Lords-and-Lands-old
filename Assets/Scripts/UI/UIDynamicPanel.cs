using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDynamicPanel : MonoBehaviour
{
    public static UIDynamicPanel Instance;

    public Button buttonDiplomacy;
    public GameObject scrollViewDiplomacy;
    public GameObject contentDiplomacy;

    public Button buttonBuilding;
    public GameObject scrollViewBuilding;
    public GameObject contentBuilding;

    public Button buttonArmy;
    public GameObject scrollViewArmy;
    public GameObject contentArmy;

    private Button[] buttonsBuilding;
    private Button[] buttonsArmy;
    public GameObject prefabButtonBuilding;
    public GameObject prefabButtonArmy;

    public Button buttonAct;
    public Button buttonDemolish;

    public Image imageAct;

    public void Init()
    {
        Instance = this;

        buttonDiplomacy.onClick.AddListener(ToggleDiplomacy);
        buttonBuilding.onClick.AddListener(ToggleBuilding);
        buttonArmy.onClick.AddListener(ToggleArmy);

        // Initialize building buttons
        buttonsBuilding = new Button[BuildingDatabase.buildings.Length];
        for (int i = 0; i < BuildingDatabase.buildings.Length; ++i)
        {
            GameObject gameobject = Instantiate(prefabButtonBuilding, contentBuilding.transform);
            gameobject.transform.SetSiblingIndex(i);

            gameobject.transform.GetChild(0).GetComponent<Image>().sprite = BuildingDatabase.buildings[i].sprite;

            Button button = gameobject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                Gameplay.currentProvince.buildingSlot.Build(BuildingDatabase.buildings[i]);
            });
            buttonsBuilding[i] = button;
        }

        // Initialize army buttons
        buttonsArmy = new Button[ArmyDatabase.armies.Length];
        for (int i = 0; i < ArmyDatabase.armies.Length; ++i)
        {
            GameObject gameobject = Instantiate(prefabButtonArmy, contentArmy.transform);
            gameobject.transform.SetSiblingIndex(i);

            GameObject prefab = Instantiate(ArmyDatabase.armies[i].prefab, gameobject.transform);

            // Crucial for prefabs to be seen
            // TODO: Make it a constant
            prefab.transform.localScale *= 150f;

            // Set the mask otherwise prefabs will be seen outside container
            //prefab.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            Utility.ConvertSpriteRendererToImage(prefab);
            for (int j = 0; j < prefab.transform.childCount; ++j)
            {
                //    prefab.transform.GetChild(j).GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                Utility.ConvertSpriteRendererToImage(prefab.transform.GetChild(j).gameObject);
            }

            Button button = gameobject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                Gameplay.currentProvince.armySlot.Recruit(ArmyDatabase.armies[i]);
            });
            buttonsArmy[i] = button;
        }

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
        //bool canBuildCapital = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Capital));
        //bool canBuildForest = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Forest));
        //bool canBuildMountains = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Mountains));
        //bool canBuildHouse = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.House));
        //bool canBuildTower = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Tower));
        //bool canBuildChurch = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.GetById(BuildingId.Church));
        //bool canRecruit = Gameplay.currentProvince.armySlot.CanRecruit(ArmyDatabase.GetById(ArmyId.Regular));
        //bool canAct = Gameplay.currentProvince.armySlot.CanAct();
        //bool canDemolish = Gameplay.currentProvince.buildingSlot.CanDemolish();
        //
        //Utility.ToggleButtonColor(Instance.buttonBuildCapital, canBuildCapital);
        //Utility.ToggleButtonColor(Instance.buttonBuildForest, canBuildForest);
        //Utility.ToggleButtonColor(Instance.buttonBuildMountains, canBuildMountains);
        //Utility.ToggleButtonColor(Instance.buttonBuildHouse, canBuildHouse);
        //Utility.ToggleButtonColor(Instance.buttonBuildTower, canBuildTower);
        //Utility.ToggleButtonColor(Instance.buttonBuildChurch, canBuildChurch);
        //Utility.ToggleButtonColor(Instance.buttonRecruit, canRecruit);
        //Utility.ToggleButtonColor(Instance.buttonAct, canAct);
        //Utility.ToggleButtonColor(Instance.buttonDemolish, canDemolish);
        //
        //
        //
        //bool availableToBuildCapital = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Capital));
        //bool availableToBuildForest = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Forest));
        //bool availableToBuildMountains = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Mountains));
        //bool availableToBuildHouse = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.House));
        //bool availableToBuildTower = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Tower));
        //bool availableToBuildChurch = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.GetById(BuildingId.Church));
        //bool availableToRecruit = Gameplay.currentProvince.armySlot.AvailableToRecruit(ArmyDatabase.GetById(ArmyId.Regular));
        //bool availableToAct = Gameplay.currentProvince.armySlot.AvailableToAct();
        //bool availableToDemolish = Gameplay.currentProvince.buildingSlot.AvailableToDemolish();
        //
        //Instance.buttonBuildCapital.gameObject.SetActive(availableToBuildCapital);
        //Instance.buttonBuildForest.gameObject.SetActive(availableToBuildForest);
        //Instance.buttonBuildMountains.gameObject.SetActive(availableToBuildMountains);
        //Instance.buttonBuildHouse.gameObject.SetActive(availableToBuildHouse);
        //Instance.buttonBuildTower.gameObject.SetActive(availableToBuildTower);
        //Instance.buttonBuildChurch.gameObject.SetActive(availableToBuildChurch);
        //Instance.buttonRecruit.gameObject.SetActive(availableToRecruit);
        //Instance.buttonAct.gameObject.SetActive(availableToAct);
        //Instance.buttonDemolish.gameObject.SetActive(availableToDemolish);

        //Instance.panelDynamic.SetActive(true);
        //UIManager.ShowTileFocus(_province.pos);
    }

    public static void HideProvince()
    {
        //Instance.panelDynamic.SetActive(false);
        //UIManager.HideTileFocus();
    }

    public static void UpdateArmyImage(Country _country)
    {
        //Instance.imageMoveArmy.sprite = AssetManager.GetArmySprite(_country);
        //Instance.imageRecruitArmy.sprite = AssetManager.GetArmySprite(_country);
    }

    public static void ToggleDiplomacy()
    {
        bool state = Instance.scrollViewDiplomacy.activeSelf;
        Instance.scrollViewDiplomacy.SetActive(!state);
    }

    public static void ToggleBuilding()
    {
        bool state = Instance.scrollViewBuilding.activeSelf;
        Instance.scrollViewBuilding.SetActive(!state);
    }

    public static void ToggleArmy()
    {
        bool state = Instance.scrollViewArmy.activeSelf;
        Instance.scrollViewArmy.SetActive(!state);
    }
}
