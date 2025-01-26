using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class LevelTransition : MonoBehaviour
{
    private GameObject[] levelList;

    [SerializeField]
    private CanvasGroup CanvasGroup;

    [SerializeField]
    private float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        levelList = FindObjectOfType<LevelList>().ListOfLevels;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("rightTrig"))
            {
                Debug.Log("triggered");

                int num = Random.Range(0, levelList.Length);

                GameObject nextLevel = Instantiate(levelList[num]);

                FadeIn();


            }
        }
    }

    private void FadeIn()
    {
        while (CanvasGroup.alpha < 1.0f)
        {
            CanvasGroup.alpha = Mathf.Lerp(CanvasGroup.alpha, 1.0f, fadeSpeed * Time.deltaTime);
        }
    }

}
