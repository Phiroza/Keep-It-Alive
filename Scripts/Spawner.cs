using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    private int randomEnemy;
    private int randomSpawnPoint;

    public float timerSpawn;
    private float timerSpawnCounter;
    public float timerReduction = 0.015f;
    public float minTimer = 0.4f;

    SignScript signScript;


    void Start()
    {
        signScript = GameObject.FindGameObjectWithTag("Sign").GetComponent<SignScript>();

        timerSpawnCounter = timerSpawn;
    }

    
    void Update()
    {
        if (timerSpawnCounter <= 0 && (signScript.numHits<4||signScript.won==false))
        {
            randomEnemy = Random.Range(0, enemies.Length);
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);            
            Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            if (timerSpawn > minTimer)
                timerSpawn -= timerReduction;
            timerSpawnCounter = timerSpawn;
        }
        else
        {
            timerSpawnCounter -= Time.deltaTime;
        }
    }
}
