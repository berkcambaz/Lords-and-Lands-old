using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Camera cam;

    public TilemapManager tilemapManager;
    public UIManager uiManager;

    private void Awake()
    {
        Instance = this;

        // Initialize managers
        tilemapManager.Init();
        uiManager.Init();

        World.Generate(4, 10, 10);
    }
}
