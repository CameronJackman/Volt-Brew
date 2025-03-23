using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject WeaponSelectMenu;


    private PlayerMovement2 playerScpt;

    private RobotScript robotScript;

    private void Start()
    {
        playerScpt = FindAnyObjectByType<PlayerMovement2>();
        robotScript = FindAnyObjectByType<RobotScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.Cursor.visible = true;
            pauseMenu.SetActive(true);


            // V freezes game so player cannot move and enemys freeze V
            Time.timeScale = 0.0f;
        }
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
        WeaponSelectMenu.SetActive(false);
    }

    public void shotGunSelection()
    {
        robotScript.rapid = false;
        robotScript.shotGun = true;
        playerScpt.resetCooldownCount = 0.9f;
        Time.timeScale = 1.0f;
        WeaponSelectMenu.SetActive(false);
    }

    public void DeafaultSelection()
    {
        robotScript.rapid = false;
        robotScript.shotGun = false;
        playerScpt.resetCooldownCount = 0.5f;
        Time.timeScale = 1.0f;
        WeaponSelectMenu.SetActive(false);
    }

}
