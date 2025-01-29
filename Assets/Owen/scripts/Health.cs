using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float playerHealth = 100;
    

    public void TakeDamage(float damage)
    {
            playerHealth -= damage;
            Debug.Log("Player Hit!");
    }

    private void Update()
    {
        

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(0);
            Debug.Log("Dead");
        }
    }
}
