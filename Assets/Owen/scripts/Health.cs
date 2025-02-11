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



    void Start()
    {
        currentHealth = maxHealth;
        
        
        
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
        

        if (currentHealth <= 0 && gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
            Debug.Log("Dead");
        }

        CheckEnemyCount();
        
    }

    public void CheckEnemyCount()
    {
        if (currentHealth <= 0 && gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
            if (spawner != null)
            {
                spawner._totalEnemies--;

                
            }
            else if (spawner == null && _dt.debugIsActive)
            {
                Debug.LogWarning("Spawner Was Null, Can't Decrement Enemies");
            }

        }
        
    }
}


    
