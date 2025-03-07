using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int currency;
    public int exp;

    public PlayerData(TestingSave testingSave, PlayerEXP playerEXP)
    {
        level = testingSave.level;
        exp = playerEXP.exp;
    }
}
