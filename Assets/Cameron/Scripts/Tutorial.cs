using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialInteract;

    [SerializeField] GameObject[] tutorialDummys;

    [SerializeField] private TMP_Text Interact;

    [SerializeField] GameObject dialogueHud;
    private bool ePress = false;

    private bool tutorialOn = false;

    private int x;
    // Start is called before the first frame update
    void Start()
    {
        Interact.CrossFadeAlpha(0.0f, 1.0f, false);

        foreach (GameObject obj in tutorialDummys)
        {
            obj.gameObject.SetActive(false);
        }

        x = Random.Range(0, tutorialDummys.Count());
    }

    // Update is called once per frame
    void Update()
    {
        if (ePress && Input.GetKeyDown(KeyCode.E))
        {
            tutorialOn = true;
            dialogueHud.SetActive(true);
        }
        

        if (tutorialOn)
        {
            tutorialDummys[x].gameObject.SetActive(true);
            Health dummyHealth = tutorialDummys[x].GetComponent<Health>();

            if (dummyHealth.currentHealth <= 0)
            {
                Debug.Log("Dummy Off");
                tutorialDummys[x].gameObject.SetActive(false);
                dummyHealth.currentHealth = 100;
                x = Random.Range(0, tutorialDummys.Count());
                
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

    
}
