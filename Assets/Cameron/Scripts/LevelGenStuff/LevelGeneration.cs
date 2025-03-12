using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class LevelGeneration : MonoBehaviour
{
    private GameObject[] levelList;

    private GameObject centerPos;

    public static int roomCount=1;
    
    private string parent = "DefaultGrid";

    



    // Start is called before the first frame update
    void Start()
    {
        levelList = FindObjectOfType<LevelList>().GreenListOfLevels;

        centerPos = gameObject.transform.GetChild(0).gameObject;

        //Debug.Log(levelList.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(roomCount);
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("leftTrig"))
            {
                int num = Random.Range(0, levelList.Length);

                float xPos = centerPos.transform.position.x - 16;
                float yPos = centerPos.transform.position.y + 8;

                

                Vector3 levelPos = new Vector3(xPos, yPos);

                GameObject nextLevel = Instantiate(levelList[num], levelPos, transform.rotation);

                TilemapRenderer tilemapRenderer = nextLevel.GetComponent<TilemapRenderer>();
                
                tilemapRenderer.sortingOrder = tilemapRenderer.sortingOrder - roomCount;

                Transform parentTransform = GameObject.Find(parent).transform;
                nextLevel.transform.SetParent(parentTransform);

                nextLevel.SetActive(true);

                Collider2D disableTrigger =GetComponent<Collider2D>();
                disableTrigger.enabled = false;


                LevelGeneration.roomCount += 1;


            }

            if (gameObject.CompareTag("rightTrig"))
            {
                int num = Random.Range(0, levelList.Length);

                float xPos = centerPos.transform.position.x + 16;
                float yPos = centerPos.transform.position.y + 8;

                

                Vector3 levelPos = new Vector3(xPos, yPos);

                GameObject nextLevel = Instantiate(levelList[num], levelPos, transform.rotation);

                TilemapRenderer tilemapRenderer = nextLevel.GetComponent<TilemapRenderer>();

                tilemapRenderer.sortingOrder -= roomCount;

                Transform parentTransform = GameObject.Find(parent).transform;
                nextLevel.transform.SetParent(parentTransform);

                nextLevel.SetActive(true);

                Collider2D disableTrigger = GetComponent<Collider2D>();
                disableTrigger.enabled = false;

                LevelGeneration.roomCount += 1;

               
            }


            
        }
    }

}
