using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;

using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class LevelTransition : MonoBehaviour
{
    private GameObject[] GreenlevelList;
    private GameObject[] RedlevelList;

    [SerializeField]
    private GameObject shopObj;

    [SerializeField]
    private GameObject levelheading;

    [SerializeField]
    private CanvasGroup _blackFade;

    [SerializeField]
    private GameObject setActiveStart;

    [SerializeField]
    private GameObject leftSpawn, rightSpawn;

    [SerializeField]
    private float fadeSpeed, fadeDuration;

    private float elapsedTime = 0;

    private bool fade, transition;

    [SerializeField] private TMP_Text Interact;
    
    private bool ePress = false;

    private GameObject spawn;

    private GameManager gameManager;

    private enemyWaveSpawns waveScript;

    // AI Scan Grid
    private bool canScan;


    //PowerUpStuff
    private GameObject[] RarePwrs;
    private GameObject[] CommonPwrs;
    public GameObject power;
    private GameObject displayPosL;
    private GameObject displayPosR;
    private bool canDisplay;
    private bool checkDis;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        GreenlevelList = FindObjectOfType<LevelList>().GreenListOfLevels;
        RedlevelList = FindObjectOfType<LevelList>().RedListOfLevels;
        shopObj = FindAnyObjectByType<LevelList>().shopLevel;

        waveScript = FindAnyObjectByType<enemyWaveSpawns>();
        

        fade = false; transition = false;

        setActiveStart.SetActive(true);

        PlayerMovement2 player = FindAnyObjectByType<PlayerMovement2>();

        if (player.spawnLocationCheck())
        {
            spawn = leftSpawn;
        }
        if (!player.spawnLocationCheck())
        {
            spawn = rightSpawn;
        }

        player.setLocation(spawn);

        Interact.CrossFadeAlpha(0.0f, 1.0f, false);

        


        if (gameManager.roomsEntered > 0)
        {
            power = ChoosePowerUp(power);
            canDisplay = false;
            checkDis = true;
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (fade)
        {
            
            _blackFade.alpha = Mathf.Lerp(_blackFade.alpha, 1.0f, fadeSpeed/ fadeDuration);

            
            
            
            
        } else if (!fade)
            {
                _blackFade.alpha = Mathf.Lerp(_blackFade.alpha, 0.0f, fadeSpeed / fadeDuration);
            }

        if (_blackFade.alpha <= 0.5f)
        {
            // Camera Zoom in
            
            
        }

        if (ePress && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.CompareTag("rightTrig"))
            {
                gameManager.EnteredNewRoom();


                //pwr2
                if (power != null)
                {
                    gameManager.storedPower = power;
                }

                transition = true;

                fade = true;
                ePress = false;

            }

            if (gameObject.CompareTag("leftTrig"))
            {
                gameManager.EnteredNewRoom();


                //pwr1 
                if (power != null)
                {
                    gameManager.storedPower = power;
                }
                
                transition = true;

                fade = true;
                ePress = false;

            }



        }


        if (_blackFade.alpha >= 0.9 && transition)
        {
            GameObject nextLevel;


            // shop every 5 levels

            if (gameManager.roomsEntered % 5 == 0)
            {
                 nextLevel = Instantiate(shopObj);
            }

            // if rooms entered is over 5 rooms then red rooms generate
            else if (gameManager.roomsEntered > 5)
            {
                int num = Random.Range(0, RedlevelList.Length);

                nextLevel = Instantiate(RedlevelList[num]);
            }


            // else just a default green level
            else
            {
                int num = Random.Range(0, GreenlevelList.Length);

                nextLevel = Instantiate(GreenlevelList[num]);
            }
            

            Transform parentTransform = GameObject.Find("DefaultGrid").transform;
            nextLevel.transform.SetParent(parentTransform);

            

            Destroy(levelheading);

            

            transition = false;

            
        }

        if (checkDis)
        {
            if (waveScript != null)
            {
                canDisplay = waveScript.Display;
            }
            
        }

        

        if (canDisplay)
        {
            if (gameManager.roomsEntered > 0)
            {
                DisplayPowers();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            
            Interact.CrossFadeAlpha(0.0f, 1.0f, false);

            ePress = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Interact.CrossFadeAlpha(1.0f, 1.0f, false);

            ePress = true;

            
        }
    }

    private void DisplayPowers()
    {

        displayPosL = GameObject.FindGameObjectWithTag("ArrowPointL");
        displayPosR = GameObject.FindGameObjectWithTag("ArrowPointR");

        if (gameObject.CompareTag("leftTrig"))
        {
            GameObject pwr1 = Instantiate(power, displayPosL.transform);
            pwr1.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Display Left");
        }

        if (gameObject.CompareTag("rightTrig"))
        {
            GameObject pwr2 = Instantiate(power, displayPosR.transform);
            pwr2.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Display right");
        }


        canDisplay = false;
        checkDis = false;
    }

    private GameObject ChoosePowerUp(GameObject pwr)
    {
        RarePwrs = FindObjectOfType<LevelList>().LessCommonPowerUps;
        CommonPwrs = FindObjectOfType<LevelList>().CommonPowerUps;


        if (RarePwrs.Count() != 0 && CommonPwrs.Count() != 0)
        {
            int num = Random.Range(0, 10);

            //if the number is 10 then give the player a rare power up choice (10%)
            if (num >= 8)
            {
                int x = Random.Range(0, RarePwrs.Count());
                pwr = RarePwrs[x];
                Debug.Log("Random Power Generated");
                // gameManager.storedPower = RarePwrs[x];
            }
            //if the number is 0-9 give the player a common power up choice (90%)
            else
            {
                int x = Random.Range(0, CommonPwrs.Count());
                pwr = CommonPwrs[x];
                Debug.Log("Random Power Generated");
                //gameManager.storedPower = CommonPwrs[x];
            }


            //displayPowerUps;
            
        }


        if (pwr == null)
        {
            Debug.LogWarning("No valid power-up selected, returning default.");
        }

        return pwr;
    }
}
