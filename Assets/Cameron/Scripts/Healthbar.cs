using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthBar;
    [SerializeField] private TMP_Text currentNum;
    [SerializeField] private Health healthScript;

    void Start()
    {
        totalhealthbar.fillAmount = healthScript.maxHealth / healthScript.maxHealth;
    }

   void Update()
    {
        currentNum.text = "" + healthScript.currentHealth + "";
        currenthealthBar.fillAmount = healthScript.currentHealth / healthScript.maxHealth;
    }
}
