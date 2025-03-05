using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public Image abilityIcon;
    public float Cooldown = 5f;
    bool isCooldown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        abilityIcon.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ability();
    }

    public void Ability()
    {
        if (Input.GetKeyUp(KeyCode.E) && isCooldown == false)
        {
            isCooldown = true;
            abilityIcon.fillAmount = 1;
        }

        if (isCooldown)
        {
            abilityIcon.fillAmount -= 1/Cooldown * Time.deltaTime;

            if (abilityIcon.fillAmount <= 0)
            {
                abilityIcon.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
