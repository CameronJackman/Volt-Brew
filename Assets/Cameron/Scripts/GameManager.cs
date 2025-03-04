using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    
    public int roomsEntered;

    private enemyWaveSpawns currentRoom;

    public int startingDifficulty;
    [HideInInspector]
    public int difficulty;

    public float secBetweenEnemySpawns;

    [SerializeField]
    private TMP_Text wavesLeftTxt, Roomstxt, enemysLeftTxt, coinsTxt;

    //[HideInInspector]
    public double coins;


    



    // Start is called before the first frame update
    void Start()
    {
        difficulty = startingDifficulty;

       
    }

    // Update is called once per frame
    void Update()
    {
        currentRoom = FindAnyObjectByType<enemyWaveSpawns>();
        
        if (currentRoom != null)
        {
            //Waves left txt
            wavesLeftTxt.text = ("Waves Left: "+ currentRoom.amountOfWaves);
            //current amount of enemys display
            enemysLeftTxt.text = ("Enemies Left: " + (currentRoom.enemiesToSpawn.Count + currentRoom.currentAmtEnemys));
        }

        // display coins

        coinsTxt.text = ("Coins: " + coins);
        
    }

    public void EnteredNewRoom()
    {
        

        if (secBetweenEnemySpawns > 1)
        {
            secBetweenEnemySpawns -= 0.25f;
        }

        roomsEntered++;
        difficulty += 1;


        // rooms entered txt
        Roomstxt.text = ("Rooms Entered: " +roomsEntered);

        difficulty = roomsEntered;
    }
}
