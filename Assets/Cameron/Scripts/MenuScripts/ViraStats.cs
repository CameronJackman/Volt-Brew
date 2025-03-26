using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ViraStats : MonoBehaviour
{
    private Health playerHealth;
    private PlayerMovement2 playerScript;
    private GameManager gameManager;

    private RobotScript projectScript;

    [SerializeField]
    private TMP_Text fireRate, shield, damage, Maxhealth;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindAnyObjectByType<PlayerMovement2>();
        playerHealth = playerScript.gameObject.GetComponent<Health>();
        gameManager = FindAnyObjectByType<GameManager>();
        projectScript = FindAnyObjectByType<RobotScript>();
    }

    // Update is called once per frame
    void Update()
    {
        fireRate.text = "Fire Rate - " + playerScript.resetCooldownCount + "ms";

        if (playerScript.isProjectileShieldOwned)
        {
            shield.text = "Shield - Owned";
        }

        damage.text = "Damage - " + projectScript.currentDamage;

        Maxhealth.text = "Max Health - " + playerHealth.maxHealth;
    }
}
