using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseWeaponScript : MonoBehaviour
{
    [SerializeField] private TMP_Text Interact;
    [SerializeField] private GameObject WeaponSelectMenu;

    private bool ePress = false;
    // Start is called before the first frame update
    void Start()
    {
        Interact.CrossFadeAlpha(0.0f, 1.0f, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ePress && Input.GetKeyDown(KeyCode.E))
        {
            WeaponSelectMenu.SetActive(true);
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
