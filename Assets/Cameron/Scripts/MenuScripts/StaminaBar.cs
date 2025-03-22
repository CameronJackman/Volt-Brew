using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Image currentStamina;
    

    private PlayerMovement2 playerScript;

    void Start()
    {
        playerScript = FindAnyObjectByType<PlayerMovement2>();
    }

    // Update is called once per frame
    void Update()
    {
        currentStamina.fillAmount = playerScript.dashCoolDownTimer;
    }
}
