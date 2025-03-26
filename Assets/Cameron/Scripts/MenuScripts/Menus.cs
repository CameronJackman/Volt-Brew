using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    private InfoMenu infoMenu;

    [HideInInspector]
    public GameObject currentOpenMenu;
    [HideInInspector]
    public bool canOpenMenu;


    private PlayerMovement2 playerScpt;

    private RobotScript robotScript;

    private void Start()
    {
        playerScpt = FindAnyObjectByType<PlayerMovement2>();
        robotScript = FindAnyObjectByType<RobotScript>();
        infoMenu = FindObjectOfType<InfoMenu>(true);

        canOpenMenu = true;
    }

    void Update()
    {
        if (!canOpenMenu && Input.GetKeyDown(KeyCode.Escape) && currentOpenMenu != null)
        {
            currentOpenMenu.SetActive(false);
            canOpenMenu = true;
            currentOpenMenu = null;
            Time.timeScale = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && canOpenMenu == true)
        {
            openPause();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            openInfoMenu();
        }

    }

    void openInfoMenu()
    {
        UnityEngine.Cursor.visible = true;
        infoMenu.gameObject.SetActive(true);
        canOpenMenu = false;
        currentOpenMenu = infoMenu.gameObject;

        // V freezes game so player cannot move and enemys freeze V
        Time.timeScale = 0.0f;
    }

    void openPause()
    {
        UnityEngine.Cursor.visible = true;
        pauseMenu.SetActive(true);
        canOpenMenu = false;
        currentOpenMenu = pauseMenu;

        // V freezes game so player cannot move and enemys freeze V
        Time.timeScale = 0.0f;
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);

        // V sets time back to normal V
        Time.timeScale = 1.0f;
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed (only works on builds)");

    }

    public void loadMainMenu()
    {
        UnityEngine.Cursor.visible = true;
        SceneManager.LoadScene(0);

    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        UnityEngine.Cursor.visible = false;
        // V sets time back to normal V
        Time.timeScale = 1.0f;
    }

    public void burstSelection()
    {
        robotScript.rapid = true;
        robotScript.shotGun = false;
        playerScpt.resetCooldownCount = 1.1f;
        Time.timeScale = 1.0f;
        currentOpenMenu.SetActive(false);
        canOpenMenu = true;
        currentOpenMenu = null;
    }

    public void shotGunSelection()
    {
        robotScript.rapid = false;
        robotScript.shotGun = true;
        playerScpt.resetCooldownCount = 0.9f;
        Time.timeScale = 1.0f;
        currentOpenMenu.SetActive(false);
        canOpenMenu = true;
        currentOpenMenu = null;
    }

    public void DeafaultSelection()
    {
        robotScript.rapid = false;
        robotScript.shotGun = false;
        playerScpt.resetCooldownCount = 0.5f;
        Time.timeScale = 1.0f;
        currentOpenMenu.SetActive(false);
        canOpenMenu = true;
        currentOpenMenu = null;
    }

}
