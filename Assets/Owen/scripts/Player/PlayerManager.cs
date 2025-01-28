using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    //Player List
    // 1) Movement (Done)
    // 2) Health and Damage system (Dia Done)
    // 3) attacking system (Cameron did mouse movement)
    // 4) Respawn at Hub system (Likely just restart the scene)
    // Start is called before the first frame update

    public GameObject player;
    public float playerHealth = 100;
    public float damageCooldown = 10f;

    public void TakeDamage(float damage)
    {
        /*if (damageCooldown > 0)
        {
            Debug.Log("Cooldown");
            return;
        }
        */
        if(damageCooldown <= 0)
        {
            playerHealth -= damage;
            Debug.Log("Player Hit!");
        }
        

    }

    private void Update()
    {
        damageCooldown -= Time.deltaTime;

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }


}
