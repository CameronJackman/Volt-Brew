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



    void Start()
    {
        currentHealth = maxHealth;
        
        gameManager = FindAnyObjectByType<GameManager>();
        
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
            curEnemySpawnScpt.currentAmtEnemys--;
            Destroy(gameObject);
        }

    }

}


    
