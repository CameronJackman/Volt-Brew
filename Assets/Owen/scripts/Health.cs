using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;

    [HideInInspector]
    public float currentHealth;
    public Spawner spawner;

    private DebugToggle _dt;

    private GameManager gameManager;

    private enemyWaveSpawns curEnemySpawnScpt;

    private Enemy enemyScript;


    void Start()
    {
        currentHealth = maxHealth;
        
        gameManager = FindAnyObjectByType<GameManager>();

        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyScript = GetComponent<Enemy>();
        }
        
    }

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    public void TakeDamage(float damage)
    {
            currentHealth -= damage;
            
            
    }

    private void Update()
    {
        //finding the current room spawns
        curEnemySpawnScpt = FindAnyObjectByType<enemyWaveSpawns>();

        if (currentHealth <= 0 && gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
            Debug.Log("Dead");
        }

        else if (currentHealth <= 0 && gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (curEnemySpawnScpt.currentAmtEnemys == 1 && curEnemySpawnScpt.enemiesToSpawn.Count == 0 && curEnemySpawnScpt.amountOfWaves == 0 && gameManager.storedPower != null)
            {
                enemyScript.DropPowerUp();
                curEnemySpawnScpt.currentAmtEnemys--;
                Destroy(gameObject);
            }
            else
            {
                curEnemySpawnScpt.currentAmtEnemys--;
                enemyScript.DropCoin();
                Destroy(gameObject);
            }
            
        }

    }

}


    
