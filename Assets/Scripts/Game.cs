using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        // Initialize managers
        tilemapManager.Init();
        uiManager.Init();
        assetManager.Init();
        armyManager.Init();

        // Initialize databases
        BuildingDatabase.Init();
    }

    public static void Quit()
    {
        Application.Quit(0);
    }
}
