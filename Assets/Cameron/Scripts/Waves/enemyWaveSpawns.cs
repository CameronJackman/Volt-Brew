using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
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
    private GameObject nextRoomInteract;
    
    private GameObject dircArrows;

    [HideInInspector]
    public int currentAmtEnemys;

    [HideInInspector]
    public bool Display;

    [SerializeField]
    private GameObject SpawnParticle;

    private GameObject ArrowL, ArrowR;

    private Animations GameAnimations;

    void Start()
    {
        GameAnimations = FindAnyObjectByType<Animations>();
        gameManager = FindAnyObjectByType<GameManager>();
        canSpawn = false;
        timeInbetweenWaves = gameManager.secBetweenEnemySpawns;
        defualtTimeBetWaves = timeInbetweenWaves;
        spawnInterval = gameManager.secBetweenEnemySpawns;
        dircArrows = GameObject.FindGameObjectWithTag("arrowSetPoint");


        //Starts GridScanOnStart after 0.05ms second to give the next room to load 
        Invoke("GridScanOnStart", 0.05f);

        ArrowL = GameObject.FindGameObjectWithTag("LeftArrow");
        ArrowR = GameObject.FindGameObjectWithTag("RightArrow");
        dircArrows.SetActive(false);
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
                        GameObject particleSpawn = Instantiate(SpawnParticle, spawnLocations[spawnPoint].position, Quaternion.identity);

                        Destroy(particleSpawn, 5);
                        GameObject Enemy = Instantiate(enemiesToSpawn[0], spawnLocations[spawnPoint].position, Quaternion.identity);
                        currentAmtEnemys++;
                        enemiesToSpawn.RemoveAt(0);
                        spawnTimer = spawnInterval;
                    }
                    else if (currentAmtEnemys == 0 && enemiesToSpawn.Count == 0 && amountOfWaves > 0)
                    {
                        amountOfWaves--;
                        GameAnimations.waveCount++;
                        GameAnimations.playWaveDisplay = true;
                        

                        GenerateWave();
                        if (spawnInterval >= 0.5)
                        {

                            /// (enemiesToSpawn.Count * 0.2f)
                            spawnInterval = gameManager.secBetweenEnemySpawns ;
                            Debug.Log(spawnInterval);
                        }

                        timeInbetweenWaves = defualtTimeBetWaves;
                    }
                    else if (currentAmtEnemys == 0)
                    {
                        // end room
                        GameAnimations.waveCount = 0;
                        waveTimer = 0;
                        nextRoomInteract.SetActive(true);
                        dircArrows.SetActive(true);
                        ArrowL.SetActive(true);
                        ArrowR.SetActive(true);
                        Display = true;
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



    //AI STUFF

    private void GridScanOnStart()
    {
        //scans Ai Nav Grid
        AstarPath.active.Scan();
    }


    //END AI STUFF

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSpawn = true;

            nextRoomInteract.SetActive(false);

            
            

        }
    }

    public void GenerateWave()
    {
       
        waveValue = difficulty * 2;
        
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