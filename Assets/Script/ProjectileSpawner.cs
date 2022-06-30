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
    [SerializeField] float baseY, sinMult, dénominateur;
    float timerSpawn;

    void Start()
    {
        timerSpawn = Random.Range(timeSpawnMin, baseY + sinMult * Mathf.Sin(Time.realtimeSinceStartup/12.75f));
    }

    void Update()
    {
        if(timerSpawn > 0 && GameManager.GameStates.InGame == GameManager.GameState)
            timerSpawn -= Time.deltaTime;
        else if(timerSpawn <= 0)
        {
            timerSpawn = Random.Range(timeSpawnMin, baseY + sinMult * Mathf.Sin(Time.realtimeSinceStartup / 12.75f));
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
            Time.timeScale = 0.5f;
            slowMotionTimer = slowMotionTime;
        }
        else if (!active)
        {
            Time.timeScale = 1f;
            Debug.Log("normal");
        }
        else if (active)
            slowMotionTimer = slowMotionTime;
    }
}
