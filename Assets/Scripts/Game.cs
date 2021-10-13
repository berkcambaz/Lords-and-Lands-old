using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    public Camera cam;

    public TilemapManager tilemapManager;

    private void Awake()
    {
        Instance = this;

        // Initialize managers
        tilemapManager.Init();

        World.Generate(4, 10, 10);
    }
}
