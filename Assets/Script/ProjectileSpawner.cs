using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    [SerializeField] int timeSpawnMin, timeSpawnMax;
    float timerSpawn;

    void Start()
    {
        
    }

    void Update()
    {
        Spawn();
    }
    void Spawn()
    {
        int direction = Random.Range(1, 4);
        Debug.Log("rnd dir : " + direction);

        Vector2 spawn;
        if(direction == 1)
        {
            spawn.x = -9.5f;
            spawn.y = Random.Range(-5f, 5f);
        }
        else if(direction == 2)
        {
            spawn.x = Random.Range(-9.5f, 9.5f);
            spawn.y = 5f;
        }
        else
        {
            spawn.x = 9.5f;
            spawn.y = Random.Range(-5f, 5f);
        }
        Instantiate(projectile, spawn, Quaternion.identity);
    }
}
