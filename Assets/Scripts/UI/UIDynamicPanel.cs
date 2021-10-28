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

    public Button buttonDemolish;
    public Button buttonAct;

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
            Building building = BuildingDatabase.buildings[i];

            GameObject gameobject = Instantiate(prefabButtonBuilding, contentBuilding.transform);
            gameobject.transform.SetSiblingIndex(i);

            gameobject.transform.GetChild(0).GetComponent<Image>().sprite = building.sprite;

            Button button = gameobject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                Gameplay.currentProvince.buildingSlot.Build(building);
            });
            buttonsBuilding[i] = button;
        }

        // Initialize army buttons
        buttonsArmy = new Button[ArmyDatabase.armies.Length];
        for (int i = 0; i < ArmyDatabase.armies.Length; ++i)
        {
            Army army = ArmyDatabase.armies[i];

            GameObject gameobject = Instantiate(prefabButtonArmy, contentArmy.transform);
            gameobject.transform.SetSiblingIndex(i);

            GameObject prefab = Instantiate(army.prefab, gameobject.transform);

            // Crucial for prefabs to be seen
            // TODO: Make it a constant
            prefab.transform.localScale *= 150f;

            // Convert sprite renderers to image so they will have masks working
            Utility.ConvertSpriteRendererToImage(prefab);
            for (int j = 0; j < prefab.transform.childCount; ++j)
            {
                Utility.ConvertSpriteRendererToImage(prefab.transform.GetChild(j).gameObject);
            }

            Button button = gameobject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                Gameplay.currentProvince.armySlot.Recruit(army);
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

    public static void FocusProvince(Province _province)
    {
        HideAll();

        // TODO: Make buttons whiter
        UIManager.ShowTileFocus(_province.pos);
    }

    public static void HideProvince()
    {
        HideAll();

        // TODO: Make buttons blacker
        UIManager.HideTileFocus();
    }

    public static void UpdateArmyImage(Country _country)
    {
        //Instance.imageMoveArmy.sprite = AssetManager.GetArmySprite(_country);
        //Instance.imageRecruitArmy.sprite = AssetManager.GetArmySprite(_country);
    }

    public static void ToggleDiplomacy()
    {
        if (Gameplay.currentProvince == null) return;

        UpdateDiplomacy();

        bool state = Instance.scrollViewDiplomacy.activeSelf;
        HideAll();
        Instance.scrollViewDiplomacy.SetActive(!state);
    }

    public static void UpdateDiplomacy()
    {
        //bool available = false;
        //bool can = false;
    }

    public static void ToggleBuilding()
    {
        if (Gameplay.currentProvince == null || Gameplay.currentProvince.owner.id != Gameplay.currentCountry.id) return;

        UpdateBuilding();

        bool state = Instance.scrollViewBuilding.activeSelf;
        HideAll();
        Instance.scrollViewBuilding.SetActive(!state);
    }

    public static void UpdateBuilding()
    {
        bool available = false;
        bool can = false;

        // "Availables" of buildings
        for (int i = 0; i < Instance.buttonsBuilding.Length; ++i)
        {
            available = Gameplay.currentProvince.buildingSlot.AvailableToBuild(BuildingDatabase.buildings[i]);
            Instance.buttonsBuilding[i].gameObject.SetActive(available);
        }

        // "Can" of buildings
        for (int i = 0; i < Instance.buttonsBuilding.Length; ++i)
        {
            can = Gameplay.currentProvince.buildingSlot.CanBuild(BuildingDatabase.buildings[i]);
            Utility.ToggleButtonColor(Instance.buttonsBuilding[i], can);
        }

        // Demolish button
        available = Gameplay.currentProvince.buildingSlot.AvailableToDemolish();
        Instance.buttonDemolish.gameObject.SetActive(available);
        can = Gameplay.currentProvince.buildingSlot.CanDemolish();
        Utility.ToggleButtonColor(Instance.buttonDemolish, can);
    }

    public static void ToggleArmy()
    {
        if (Gameplay.currentProvince == null || Gameplay.currentProvince.owner.id != Gameplay.currentCountry.id) return;

        UpdateArmy();

        bool state = Instance.scrollViewArmy.activeSelf;
        HideAll();
        Instance.scrollViewArmy.SetActive(!state);
    }

    public static void UpdateArmy()
    {
        bool available = false;
        bool can = false;

        // "Availables" of armies
        for (int i = 0; i < Instance.buttonsArmy.Length; ++i)
        {
            available = Gameplay.currentProvince.armySlot.AvailableToRecruit(ArmyDatabase.armies[i]);
            Instance.buttonsArmy[i].gameObject.SetActive(available);
        }

        // "Can" of armies
        for (int i = 0; i < Instance.buttonsArmy.Length; ++i)
        {
            can = Gameplay.currentProvince.armySlot.CanRecruit(ArmyDatabase.armies[i]);
            Utility.ToggleButtonColor(Instance.buttonsArmy[i], can);
        }

        // Act button
        available = Gameplay.currentProvince.armySlot.AvailableToAct();
        Instance.buttonAct.gameObject.SetActive(available);
        can = Gameplay.currentProvince.armySlot.CanAct();
        Utility.ToggleButtonColor(Instance.buttonAct, can);
    }

    private static void HideAll()
    {
        Instance.scrollViewDiplomacy.SetActive(false);
        Instance.scrollViewBuilding.SetActive(false);
        Instance.scrollViewArmy.SetActive(false);
    }
}
