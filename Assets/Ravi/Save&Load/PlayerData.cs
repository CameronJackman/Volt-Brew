using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int currency;
    public int exp;

    public PlayerData(TestingSave player)
    {
        level = player.level;
    }

    public PlayerData(PlayerEXP player)
    {
        exp = player.exp;
    }
}
