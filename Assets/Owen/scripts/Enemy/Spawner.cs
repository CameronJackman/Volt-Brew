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
    public int waveCount = 0;
    public float waveBufferTimer = 5f;

    public int _desiredEnemies = 5;
    public float _spawnRate = 0.25f;
    int difficultyMuliplier = 2;

    private float _timeSinceLastSpawn = 0.0f;

    private bool _canSpawn = false;
    public int _totalEnemies { get; set; }
    public int _enemyCount = 0;
    public string waveName { get; set; }

    


    

    private void Update()
    {
        if (IsCombatMap(true))
        {
            if (_enemyCount == _desiredEnemies && _totalEnemies == 0 && waveBufferTimer >= 0f)
            {
                waveBufferTimer -= Time.deltaTime;
                Debug.Log(waveBufferTimer);
            }
            if (_totalEnemies <= 0 && canStartWaves && waveBufferTimer <= 0f)
            {
                StartNextWave();
                canStartWaves = false;
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
                _desiredEnemies = 5 + waveCount * difficultyMuliplier;
                Debug.Log(_desiredEnemies + ": " + waveCount);
                waveCount++;
                _enemyCount = 0;
                

                Debug.Log("Starting Wave " + waveCount + "with" + _desiredEnemies + "enemies");
            }
            else if (waveCount == 3)
            {
                Debug.Log("Wave 3 Complete: Next Level");
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

        if (_dt.debugIsActive)
        {
            Debug.LogWarning("No Option To Set Difficulty Is Programmmed");
            Debug.LogWarning("Add in a difficulty setting to the code to program harder waves and set the difficulty multiplier inside of this method instead of at the top");
        }
         
        if (easy)
        {
            difficultyMuliplier = waveEnemyMultiplier * 1;
        }
            
        if (hard)
        {
            difficultyMuliplier = waveEnemyMultiplier * 3;
        }
            
        if (nightmare)
        {
            difficultyMuliplier = waveEnemyMultiplier * 5;
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

