using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class LevelTransition : MonoBehaviour
{
    private GameObject[] levelList;

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



    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        levelList = FindObjectOfType<LevelList>().ListOfLevels;
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

            if (gameManager.roomsEntered % 5 == 0)
            {
                 nextLevel = Instantiate(shopObj);
            }
            else
            {
                int num = Random.Range(0, levelList.Length);

                nextLevel = Instantiate(levelList[num]);
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
