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

    public int difficulty;

    public float secBetweenEnemySpawns;

    [SerializeField]
    private TMP_Text wavesLeftTxt, Roomstxt, enemysLeftTxt, coinsTxt;

    //[HideInInspector]
    public double coins;


    //hide this
    public GameObject storedPower;
    



    // Start is called before the first frame update
    void Start()
    {
        

       
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

        coinsTxt.text = ("Scrap: " + coins);
        
    }

    public void EnteredNewRoom()
    {
        

        if (secBetweenEnemySpawns > 1)
        {
            secBetweenEnemySpawns -= 0.25f;
        }

        roomsEntered++;
        difficulty = roomsEntered;
        difficulty += 5;


        // rooms entered txt
        Roomstxt.text = ("Rooms Entered: " +roomsEntered);

        
    }
}
