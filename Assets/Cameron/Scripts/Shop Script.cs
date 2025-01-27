using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private TMP_Text Interact;
    [SerializeField] private GameObject shopMenu;
    private bool ePress = false;
    // Start is called before the first frame update
    void Start()
    {
        //Sets Interact text to invisible start game
        Interact.CrossFadeAlpha(0.0f, 1.0f, false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //set shop menu active
        if (ePress == true && Input.GetKeyDown(KeyCode.E))
        {
            shopMenu.SetActive(true);

            Time.timeScale = 0.0f;
        }

        //Set shop menu inactive
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //Fades Interact Text when player leaves trigger box 
            Interact.CrossFadeAlpha(0.0f, 1.0f, false);

            ePress = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //fades interact text when player enters trigger box 
            Interact.CrossFadeAlpha(1.0f, 1.0f, false);

            ePress = true;
            
        }
    }

    public void CloseMenu()
    {
        Time.timeScale = 1.0f;
    }

    
}
