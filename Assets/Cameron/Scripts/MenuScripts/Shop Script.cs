using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private TMP_Text Interact;
    private shopHud shopMenu;
    private bool ePress = false;

    private Menus menuScript;
    


    // Start is called before the first frame update
    void Start()
    {
        //Sets Interact text to invisible start game
        Interact.CrossFadeAlpha(0.0f, 1.0f, false);

        
       
        
        shopMenu = FindObjectOfType<shopHud>(true);
        shopMenu.healthBought = false;

        shopMenu.gameObject.SetActive(false);



        menuScript = FindAnyObjectByType<Menus>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set shop menu active
        if (ePress == true && Input.GetKeyDown(KeyCode.E) && menuScript.canOpenMenu && shopMenu != null )
        {
            UnityEngine.Cursor.visible = true;
            shopMenu.gameObject.SetActive(true);
            menuScript.canOpenMenu = false;
            menuScript.currentOpenMenu = shopMenu.gameObject;
            
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

    

    

    
}
