using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class shopHud : MonoBehaviour
{
    [SerializeField]
    public GameObject[] resetOnStart;

    [HideInInspector]
    public bool shieldOwned, healthBought, powerBought;

    private PlayerMovement2 playerMovement;
    private GameManager gameManager;
    private Health playerHealth;
    


    //Costs of shop items
    [SerializeField]
    private float healthCost;
    [SerializeField]
    private float projectileShieldCost;
    [SerializeField]
    private float MaxHealthCost;
    [SerializeField]
    private float decreaseFireRateCost;
    [SerializeField]
    private float damageUpgradeCost;
    [SerializeField]
    private float jackPotCost;
    [SerializeField]
    private float reRollCost;

    private Menus menuScript;

    public GameObject[] commonPowerMenus;
    public GameObject[] rarePowerMenus;

    private int ranNum;

    private bool displayingRarePower;

    private int reRollCount = 0;

    [SerializeField]
    private TMP_Text reRollText;

    private GameObject currentDisplayingBar;

    private Animations GameAnimations;

    // Start is called before the first frame update
    void Start()
    {
        GameAnimations = FindAnyObjectByType<Animations>();
        menuScript = FindAnyObjectByType<Menus>();
        playerMovement = FindAnyObjectByType<PlayerMovement2>();
        playerHealth = playerMovement.GetComponent<Health>();
        gameManager = FindAnyObjectByType<GameManager>();

        ranNum = Random.Range(0, 11);
        RandomPower();
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

        if (powerBought)
        {
            resetOnStart[2].SetActive(true);
        }
        else if (!powerBought)
        {
            resetOnStart[2].SetActive(false);
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
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.itemBought);
            healthBought = true;
            Debug.Log("Health Bought");
        }
        else
        {
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.invalid);
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
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.itemBought);
            shieldOwned = true;



        }
        else
        {
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.invalid);
        }
    }

    public void DamageUpgrade()
    {
        if (gameManager.coins >= damageUpgradeCost)
        {
            gameManager.coins -= damageUpgradeCost;
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
            powerBought = true;

            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.itemBought);
        }
        else
        {
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.invalid);
        }
    }
    public void MaxHealthIncrease()
    {
        if (gameManager.coins >= MaxHealthCost)
        {
            playerHealth.maxHealth += 15;
            playerHealth.currentHealth += 15;
            gameManager.coins -= MaxHealthCost;
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.itemBought);
            powerBought = true;
        }
        else
        {
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.invalid);
        }
    }

    public void FireRateDecrease()
    {
        if (playerMovement.resetCooldownCount > 0.1)
        {
            if (gameManager.coins >= decreaseFireRateCost)
            {
                playerMovement.resetCooldownCount -= 0.1f;
                gameManager.coins -= decreaseFireRateCost;
                GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.itemBought);
                powerBought = true;
            }
            else
            {
                GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.invalid);
            }
        }
        else
        {
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.invalid);
            powerBought = false;
        }

            
    }

    public void JackPot()
    {
        if (gameManager.coins >= jackPotCost)
        {
            gameManager.coins -= jackPotCost;
            gameManager.coins += 1000;
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.itemBought);
            powerBought = true;
        }
        else
        {
            GameAnimations.globalAudioSource.PlayOneShot(GameAnimations.invalid);
        }
            
    }

    public void ReRoll()
    {
        if (reRollCount < 3 && gameManager.coins >= reRollCost)
        {
            gameManager.coins -= reRollCost;

            ranNum = Random.Range(0, 11);

            reRollCount++;

            reRollText.text = "ReRoll PowerUp " + reRollCount + "/3";

            currentDisplayingBar.SetActive(false);

            RandomPower();
        }
    }

    public void RandomPower()
    {
        if (ranNum == 10)
        {
            int i = Random.Range(0, rarePowerMenus.Count());

            Debug.Log(i);

            rarePowerMenus[i].gameObject.SetActive(true);


            currentDisplayingBar = rarePowerMenus[i].gameObject;

            displayingRarePower = true;

        }
        else if (ranNum != 10)
        {
            int i = Random.Range(0, commonPowerMenus.Count());

            commonPowerMenus[i].gameObject.SetActive(true);

            currentDisplayingBar = commonPowerMenus[i].gameObject;

            displayingRarePower = false;
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
