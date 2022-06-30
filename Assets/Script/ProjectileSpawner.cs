using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    public List<GameObject> projectiles = new List<GameObject>();
    public float projectileSpeed;
    public float speedVariation;
    public float slowMotionTime;
    [SerializeField]float slowMotionTimer;


    [SerializeField] int timeSpawnMin, timeSpawnMax;
    float timerSpawn;

    void Start()
    {
        timerSpawn = Random.Range(timeSpawnMin, timeSpawnMax);
    }

    void Update()
    {
        if(timerSpawn > 0 && GameManager.GameStates.InGame == GameManager.GameState)
            timerSpawn -= Time.deltaTime;
        else if(timerSpawn <= 0)
        {
            timerSpawn = Random.Range(timeSpawnMin, timeSpawnMax);
            Spawn();
        }

        if (slowMotionTimer > 0 && GameManager.GameStates.InGame == GameManager.GameState)
            slowMotionTimer -= Time.deltaTime;
        else if (slowMotionTimer < 0)
        {
            slowMotionTimer = 0;
            SlowMotion(false);
        }

        for (int i = 0; i < projectiles.Count; i++)
            if (projectiles[i] == null)
                projectiles.RemoveAt(i);
    }
    void Spawn()
    {
        int direction = Random.Range(1, 4);

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
        Instantiate(projectile, spawn, Quaternion.identity, transform);
    }
    public void DestroyAll()
    {
        for(int i = 0; i < projectiles.Count; i++)
        {
            Destroy(projectiles[i]);
            projectiles.RemoveAt(i);
        }
    }
    public void SlowMotion(bool active)
    {
        if (active && slowMotionTimer == 0)
        {
            projectileSpeed -= speedVariation;
            slowMotionTimer = slowMotionTime;
        }
        else if(!active && slowMotionTimer == 0)
            projectileSpeed += speedVariation;
        else
            slowMotionTimer = slowMotionTime;
        for (int i = 0; i < projectiles.Count; i++)
        {
            projectiles[i].GetComponent<Projectile>().ChangeVelocity();
            Debug.Log(active);
        }
    }
}
