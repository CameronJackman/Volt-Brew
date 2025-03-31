using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUps : MonoBehaviour
{
    public enum PowerUpList
    {
        Health,
        Coins,
        Sheild,
        HealthUpgrade,
        FireRateUpgrade,
        DamageUpgrade
    }

    public PowerUpList PowerUpChoice;

    private PlayerMovement2 playerScript;
    private GameManager gameManager;

    private Animations GameAnimations;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindAnyObjectByType<PlayerMovement2>();
        gameManager = FindAnyObjectByType<GameManager>();
        GameAnimations = FindAnyObjectByType<Animations>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PowerUpChoice == PowerUpList.Health)
            {
                healthPwer();
            }

            if (PowerUpChoice == PowerUpList.Coins)
            {
                coinPwer();
            }

            if (PowerUpChoice == PowerUpList.Sheild)
            {
                GrantSheild();
            }
            if (PowerUpChoice == PowerUpList.HealthUpgrade)
            {
                MoreHealth();
            }
            if (PowerUpChoice == PowerUpList.FireRateUpgrade)
            {
                fasterFireRate();
            }
            if (PowerUpChoice == PowerUpList.DamageUpgrade)
            {
                DamageUpgrade();
            }


            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.pwrPickupClip);
            //destroys the game object after collision and powerup put into effect
            Destroy(gameObject);
        }
    }

    public void DamageUpgrade()
    {
        RobotScript rbS = FindAnyObjectByType<RobotScript>();

        if (rbS != null)
        {
            if (rbS.rapid)
            {
                rbS.rapidDamage += 25;
            }
            else if (rbS.shotGun)
            {
                rbS.shotGunDamage += 25;
            }
            else if (!rbS.rapid && !rbS.shotGun)
            {
                rbS.singleDamage += 25;
            }

        }
    }

    //gives the player back 50 HP
    public void healthPwer()
    {
        Health playerHealth = playerScript.gameObject.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.currentHealth += 50;

            if (playerHealth.currentHealth > playerHealth.maxHealth)
            {
                playerHealth.currentHealth = playerHealth.maxHealth;
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

    //Upgrades health
    public void MoreHealth()
    {
        Health playerHealth = playerScript.gameObject.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.maxHealth += 15;
            playerHealth.currentHealth += 15;
        }
    }

    public void fasterFireRate()
    {
        if (playerScript != null)
        {
            if (playerScript.resetCooldownCount > 0.1)
            {
                playerScript.resetCooldownCount -= 0.1f;
            }
        }
    }
}
