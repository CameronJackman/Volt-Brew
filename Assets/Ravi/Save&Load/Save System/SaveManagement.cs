using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManagement : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // Save Game
        {
            FindObjectOfType<TestingSave>().SavePlayerLevel();
        }

        if (Input.GetKeyDown(KeyCode.L)) // Load Game
        {
            FindObjectOfType<TestingSave>().LoadPlayerLevel();
            FindObjectOfType<PlayerEXP>().LoadPlayerEXP();
        }
    }
}
