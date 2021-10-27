using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Camera cam;

    public TilemapManager tilemapManager;
    public UIManager uiManager;
    public AssetManager assetManager;
    public ArmyManager armyManager;

    private void Awake()
    {
        Instance = this;

        // Initialize databases
        ArmyDatabase.Init();
        BuildingDatabase.Init();

        // Initialize managers
        tilemapManager.Init();
        uiManager.Init();
        assetManager.Init();
        armyManager.Init();
    }

    public static void Quit()
    {
        Application.Quit(0);
    }
}
