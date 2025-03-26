using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class shopHud : MonoBehaviour
{
    [SerializeField]
    public GameObject[] resetOnStart;

    [HideInInspector]
    public bool shieldOwned, healthBought;

    private PlayerMovement2 playerMovement;
    private GameManager gameManager;
    private Health playerHealth;


    //Costs of shop items
    [SerializeField]
    private float healthCost;
    [SerializeField]
    private float projectileShieldCost;

    private Menus menuScript;


    // Start is called before the first frame update
    void Start()
    {
        menuScript = FindAnyObjectByType<Menus>();
        playerMovement = FindAnyObjectByType<PlayerMovement2>();
        playerHealth = playerMovement.GetComponent<Health>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Awake()
    {
        for (int i = 0; i < resetOnStart.Count(); i++)
        {
            resetOnStart[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shieldOwned)
        {
            resetOnStart[1].SetActive(true);
        }

        if (healthBought)
        {
            resetOnStart[0].SetActive(true);
        }
        else if (!healthBought)
        {
            resetOnStart[0].SetActive(false);
        }
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

            healthBought = true;
            Debug.Log("Health Bought");
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

            shieldOwned = true;



        }
    }

    public void CloseMenu()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        UnityEngine.Cursor.visible = false;
        menuScript.canOpenMenu = true;
    }
}
