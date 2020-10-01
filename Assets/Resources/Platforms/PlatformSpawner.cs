﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Public References
    public GameObject spawnPoint;
    public GameObject powerup;
    public EnemySpawner enemySpawner;

    // Private Data
    public enum SpawnType { ENEMY, POWER_UP };
    

    // Issues to Spawn on to of Platform
    public void spawn(SpawnType type) {
        switch (type) {
            case SpawnType.ENEMY:
                GameObject enemy = enemySpawner.getEnemy();
                enemy = Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
                enemy.transform.parent = transform;
                break;
                
            case SpawnType.POWER_UP:
                Debug.Log("Spawning Power Up!");
                Instantiate(powerup, spawnPoint.transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
    
    
}
