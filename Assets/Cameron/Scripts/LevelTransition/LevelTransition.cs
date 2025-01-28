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

   
    // Start is called before the first frame update
    void Start()
    {
        levelList = FindObjectOfType<LevelList>().ListOfLevels;

        fade = false; transition = false;

        setActiveStart.SetActive(true);

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();

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

            

            Debug.Log("fading");
            
        } else if (!fade)
            {
                _blackFade.alpha = Mathf.Lerp(_blackFade.alpha, 0.0f, fadeSpeed / fadeDuration);
            }



        if (ePress && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.CompareTag("rightTrig") || gameObject.CompareTag("leftTrig"))
            {

                transition = true;

                fade = true;
                ePress = false;

            }

            
            
        }


        if (_blackFade.alpha >= 0.99 && transition)
        {
            Debug.Log("triggered");

            int num = Random.Range(0, levelList.Length);

            GameObject nextLevel = Instantiate(levelList[num]);

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
