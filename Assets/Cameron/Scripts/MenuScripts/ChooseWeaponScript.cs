using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ChooseWeaponScript : MonoBehaviour
{
    [SerializeField] private TMP_Text Interact;
    [SerializeField] private GameObject WeaponSelectMenu;
    private Menus menuScript;

    private bool ePress = false;
    // Start is called before the first frame update
    void Start()
    {
        Interact.CrossFadeAlpha(0.0f, 1.0f, false);
        menuScript = FindAnyObjectByType<Menus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ePress && Input.GetKeyDown(KeyCode.E) && menuScript.canOpenMenu)
        {
            WeaponSelectMenu.SetActive(true);
            menuScript.canOpenMenu = false;
            menuScript.currentOpenMenu = WeaponSelectMenu;
            Time.timeScale = 0;
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
