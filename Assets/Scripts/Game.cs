using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public TilemapManager tilemapManager;

    private void Awake()
    {
        // Initialize managers
        tilemapManager.Init();

        World.Generate(4, 10, 10);
    }
}
