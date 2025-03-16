using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private TMP_Text Interact;
    [SerializeField] private GameObject shopMenu;
    private bool ePress = false;

    private Health playerHealth;
    private PlayerMovement2 playerMovement;
    private GameManager gameManager;

    private bool canBuy = true;

    [SerializeField]
    //Costs of shop items
    private float healthCost;
    public float projectileShieldCost;

    

    // Start is called before the first frame update
    void Start()
    {
        //Sets Interact text to invisible start game
        Interact.CrossFadeAlpha(0.0f, 1.0f, false);

        playerMovement = FindAnyObjectByType<PlayerMovement2>();
        playerHealth = playerMovement.GetComponent<Health>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set shop menu active
        if (ePress == true && Input.GetKeyDown(KeyCode.E) && shopMenu != null && canBuy == true)
        {
            shopMenu.SetActive(true);

            Time.timeScale = 0.0f;
        }

        //Set shop menu inactive

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //Fades Interact Text when player leaves trigger box 
            Interact.CrossFadeAlpha(0.0f, 1.0f, false);

            ePress = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //fades interact text when player enters trigger box 
            Interact.CrossFadeAlpha(1.0f, 1.0f, false);

            ePress = true;
            
        }
    }

    public void CloseMenu()
    {
        Time.timeScale = 1.0f;
        shopMenu.SetActive(false);
    }

    //SHOP ITEM #1 --> Health Pot
    public void HealthPot()
    {
        if (gameManager.coins >= healthCost)
        {

            playerHealth.currentHealth += 25;
            gameManager.coins -= healthCost;

            if (playerHealth.currentHealth > playerHealth.maxHealth)
            {
                playerHealth.currentHealth = playerHealth.maxHealth;
            }

            canBuy = false; //include this at end of every buff/power up for the shop so the shop cant be opened again
            CloseMenu();
        } 
    }

    //SHOP ITEM #2 --> Projectile Shield
    public void ProjectileShield()
    {
        if (gameManager.coins >= projectileShieldCost)
        {
            //Activate projectile shield  --> Need to add a display & code for shields remaining
            playerMovement.ActivateProjectileShield();

            //Calculate the player's current balance after purchase
            gameManager.coins -= projectileShieldCost;

            playerMovement.isProjectileShieldOwned = true;

            //Include this at end of every buff/power up for the shop so the shop cant be opened again
            canBuy = false; 
            CloseMenu();
        }
    }

    
}
