using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WormSpawner wormSpawner;
    
    private void Start()
    {
        for (int i = 0; i < 4; i++)
            wormSpawner.SpawnWorm();
    }
}
