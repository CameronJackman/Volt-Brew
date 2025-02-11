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


    void Start()
    {
        currentHealth = maxHealth;
        
    }

    public void TakeDamage(float damage)
    {
            currentHealth -= damage;
            Debug.Log("Player Hit!");
    }

    private void Update()
    {
        

        if (currentHealth <= 0 && gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
            Debug.Log("Dead");
        }
        else if (currentHealth <= 0 && gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}


    
