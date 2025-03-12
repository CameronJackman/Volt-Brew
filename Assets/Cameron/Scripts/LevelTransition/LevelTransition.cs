using System.Collections;
using System.Collections.Generic;
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

    // AI Scan Grid
    private bool canScan;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        GreenlevelList = FindObjectOfType<LevelList>().GreenListOfLevels;
        RedlevelList = FindObjectOfType<LevelList>().RedListOfLevels;
        shopObj = FindAnyObjectByType<LevelList>().shopLevel;

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



        if (ePress && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.CompareTag("rightTrig") || gameObject.CompareTag("leftTrig"))
            {
                gameManager.EnteredNewRoom();
                
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

    

}
