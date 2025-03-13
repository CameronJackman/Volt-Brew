using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUps : MonoBehaviour
{
    public enum PowerUpList
    {
        Health,
        Coins,
        Sheild
    }

    public PowerUpList PowerUpChoice;

    private PlayerMovement2 playerScript;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindAnyObjectByType<PlayerMovement2>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PowerUpChoice == PowerUpList.Health)
        {
            healthPwer();
            Destroy(gameObject);
        }

        if (PowerUpChoice == PowerUpList.Coins)
        {
            coinPwer();
            Destroy(gameObject);
        }

        if (PowerUpChoice == PowerUpList.Sheild)
        {
            GrantSheild();
            Destroy(gameObject);
        }
    }

    //gives the player back 50 HP
    public void healthPwer()
    {
        Health playerHealth = playerScript.gameObject.GetComponent<Health>();

        if (playerHealth != null)
        {

            if (playerHealth.currentHealth >= 50)
            {
                playerHealth.currentHealth = 100;
            }
            else
            {
                playerHealth.currentHealth += 50;
            }
                

            
        }
    }


    //Gives Player 100 Coins
    public void coinPwer()
    {
        if (gameManager != null)
        {
            gameManager.coins += 100;
        }
    }


    //Gives Player Sheild
    public void GrantSheild()
    {
        if (playerScript != null)
        {
            playerScript.isProjectileShieldOwned = true;
        }
    }
}
