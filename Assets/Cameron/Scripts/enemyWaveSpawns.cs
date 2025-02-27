using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class enemyWaveSpawns : MonoBehaviour
{
    public List<waveEnemy> enemies = new List<waveEnemy>();
    public int difficulty;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public List<Transform> spawnLocations = new List<Transform>();
    private int howFastEnemysSpawn = 3;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    private float timeElasped;

    
    public int amountOfWaves = 3;

    private float timeInbetweenWaves;
    private float defualtTimeBetWaves;

    private GameManager gameManager;

    private bool canSpawn = false;
    [SerializeField]
    private GameObject doorBar;
    [SerializeField]
    private GameObject dircArrows;

    [HideInInspector]
    public int currentAmtEnemys;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        canSpawn = false;
        timeInbetweenWaves = gameManager.secBetweenEnemySpawns;
        defualtTimeBetWaves = timeInbetweenWaves;
        spawnInterval = howFastEnemysSpawn;
    }

    void FixedUpdate()
    {

        if (canSpawn)
        {
            
            difficulty = gameManager.difficulty;

           

            if (timeInbetweenWaves <= 0)
            {
                if (spawnTimer <= 0)
                {
                    //spawn enemy
                    if (enemiesToSpawn.Count > 0 && spawnTimer <= 0)
                    {
                        int spawnPoint = Random.Range(0, spawnLocations.Count);
                        Instantiate(enemiesToSpawn[0], spawnLocations[spawnPoint].position, Quaternion.identity);
                        currentAmtEnemys++;
                        enemiesToSpawn.RemoveAt(0);
                        spawnTimer = spawnInterval;
                    }
                    else if (currentAmtEnemys == 0 && enemiesToSpawn.Count == 0 && amountOfWaves > 0)
                    {
                        amountOfWaves--;
                        
                        GenerateWave();
                        if (spawnInterval >= 0.5)
                        {
                            spawnInterval = howFastEnemysSpawn / (enemiesToSpawn.Count * 0.2f);
                            Debug.Log(spawnInterval);
                        }

                        timeInbetweenWaves = defualtTimeBetWaves;
                    }
                    else if (currentAmtEnemys == 0)
                    {
                        // end room
                        waveTimer = 0;
                        doorBar.SetActive(false);
                        dircArrows.SetActive(true);
                    }
                }
                else
                {
                    spawnTimer -= Time.fixedDeltaTime;
                    waveTimer -= Time.fixedDeltaTime;
                }
            }

            if (timeInbetweenWaves >= -5)
            {
                timeInbetweenWaves -= Time.deltaTime;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSpawn = true;

            doorBar.SetActive(true);

        }
    }

    public void GenerateWave()
    {
       
        waveValue = difficulty * 10;
        
        GenerateEnemies();

        
        waveTimer = 3;

        Debug.Log(spawnInterval);
    }


    public void GenerateEnemies()
    {

        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }

        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;

    }

    public void Update()
    {
    }

}


[System.Serializable]

public class waveEnemy
{
    public GameObject enemyPrefab;
    public int cost;
}