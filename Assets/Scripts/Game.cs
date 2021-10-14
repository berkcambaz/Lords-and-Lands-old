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

    private void Awake()
    {
        Instance = this;

        // Initialize managers
        tilemapManager.Init();
        uiManager.Init();
        assetManager.Init();

        World.Generate(4, 10, 10);
    }
}
