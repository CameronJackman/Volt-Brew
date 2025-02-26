using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform _spawnParent;

    private DebugToggle _dt;

    public List<Enemy> _enemyTypes = new List<Enemy>();
    public List<Transform> _spawnPoints = new List<Transform>();
    private bool easy, hard, nightmare = false;
    public int waveEnemyMultiplier = 2;
    
    private bool isCombatLevel = false;
    private bool canStartWaves = true;
    public int waveCount = 1;
    public float waveBufferTimer = 5f;
    private bool hasDisplayed = false;

    public int _desiredEnemies = 5;
    public float _spawnRate = 0.25f;
    int difficultyMuliplier;

    private float _timeSinceLastSpawn = 0.0f;

    private bool _canSpawn = false;
    public int _totalEnemies { get; set; }
    public int _enemyCount = 0;
    public string waveName { get; set; }

    


    
    void Start()
    {
        _enemyCount = _desiredEnemies;
        difficultyMuliplier = Random.Range(8, 15);
    }
    private void Update()
    {
        if (IsCombatMap(true))
        {
            if (_totalEnemies <= 0 && canStartWaves && waveBufferTimer <= 0f)
            {
                StartNextWave();
                canStartWaves = false;
                waveBufferTimer = 5f;
            }
            if (_enemyCount == _desiredEnemies && _totalEnemies == 0 && waveBufferTimer >= 0f)
            {
                waveBufferTimer -= Time.deltaTime;
                
            }
            
            
            UpdateSpawnTimer();
            SpawnDesiredEnemies();

            
        }
        

        //CheckDifficulty();
        
        

    }

   

    void StartNextWave()
    {
        if (waveBufferTimer <= 0f)
        {
            if (waveCount < 3 && _totalEnemies == 0)
            {
                waveCount++;
                _desiredEnemies = Random.Range(5,7) + waveCount * difficultyMuliplier;
                Debug.Log(_desiredEnemies + ": " + waveCount);
                _enemyCount = 0;
                

                Debug.Log("Starting Wave " + waveCount + "with" + _desiredEnemies + "enemies");
            }
            else if (waveCount == 3 && !hasDisplayed)
            {
                Debug.Log("Wave 3 Complete: Next Level");
                hasDisplayed = true;
            }
        }
        
        
    }


    private bool IsCombatMap(bool isCombatMap)
    {

        isCombatMap = isCombatLevel;
        if (gameObject.CompareTag("CombatZone"))
        {
            isCombatMap = true;
            isCombatLevel = true;
        }
        else if (gameObject.CompareTag("NotCombatZone"))
        {
            isCombatLevel = false;
            isCombatMap = false;
        }

        return isCombatMap;
    }

    public int CheckDifficulty()  
    {

        if (DebugToggle._debug)
        {
            Debug.LogWarning("No Option To Set Difficulty Is Programmmed");
            Debug.LogWarning("Add in a difficulty setting to the code to program harder waves and set the difficulty multiplier inside of this method instead of at the top");
        }
         
        if (easy)
        {
            difficultyMuliplier = waveEnemyMultiplier * 2;
        }
            
        if (hard)
        {
            difficultyMuliplier = waveEnemyMultiplier * 8;
        }
            
        if (nightmare)
        {
            difficultyMuliplier = waveEnemyMultiplier * 13;
        }
            

        return difficultyMuliplier;
    }

   



    private void UpdateSpawnTimer()
    {
        
        
        if (!_canSpawn && isCombatLevel)
        {
            if (_timeSinceLastSpawn >= _spawnRate)
            {
                _canSpawn = true;
                _timeSinceLastSpawn = 0.0f;
            }

            _timeSinceLastSpawn += Time.deltaTime;
        }
    }

    private void SpawnDesiredEnemies()
    {
        if (isCombatLevel)
        {
            canStartWaves = true;
            

            if (_totalEnemies < _desiredEnemies)
            {
                
                
                if (_canSpawn && _enemyCount != _desiredEnemies)
                {
                    SpawnRandomEnemy();
                    _canSpawn = false;
                }
            }
        }
        
        else if (!isCombatLevel)
        {
            canStartWaves = false;
        }
    }

    private void SpawnRandomEnemy()
    {
        _enemyCount++;
        //Debug.Log(_totalEnemies);
        Debug.Log("Enemy Count: " + _enemyCount);
        int enemyType = Random.Range(0, _enemyTypes.Count);
        int spawnPoint = Random.Range(0, _spawnPoints.Count);

        Instantiate(_enemyTypes[enemyType], _spawnPoints[spawnPoint].position, _spawnPoints[spawnPoint].rotation, _spawnParent);
        _totalEnemies++;

        
        
    }

    private void Awake()
    {
        _dt = FindObjectOfType<DebugToggle>();
    }
}

