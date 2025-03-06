using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public void SavePlayer() => SaveSystem.SavePlayer();

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
    }
}
