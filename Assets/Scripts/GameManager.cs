using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager {  get; private set; }
   

    public HealthSystem playerHealth = new HealthSystem(10, 10);

    [field: Header("Menus")]
    public GameObject gameOverScreen;
    public GameObject pauseMenu;
    public GameObject pauseMainMenu;
    public GameObject pauseSettingsManu;
    public GameObject exitCheck;
    private bool isPaused = false;

    void Awake()
    {
        gameOverScreen.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        exitCheck.gameObject.SetActive(false);
        pauseSettingsManu.gameObject.SetActive(false);

        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

    public void Update()
    {
        if (GameObject.FindWithTag("Player") == null )
        {
            gameOverScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        //pause Game
        if (Input.GetKeyDown(KeyCode.P) & GameObject.FindWithTag("Player") != null)
        {
            pauseMenu.gameObject.SetActive(!isPaused);
            isPaused = !isPaused;
            Time.timeScale = 0f;
        }
    }

    public void resumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void openSettingsInPause()
    {
        pauseMainMenu.gameObject.SetActive(false);
        pauseSettingsManu.gameObject.SetActive(true);
    }

    public void exitToMenuCheck()
    {
        pauseMainMenu.gameObject.SetActive(false);
        exitCheck.gameObject.SetActive(true);
    }

    public void exitToMenuYes()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void exitToMenuNo()
    {
        exitCheck.gameObject.SetActive(false);
        pauseMainMenu.gameObject.SetActive(true);
    }

    public void fullScreenChange()
    {
        if (Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
            Debug.Log("went fullscreen");
        }
        else if (Screen.fullScreen == true)
        {
            Screen.fullScreen = false;
            Debug.Log("went windowed");
        }
    }

    public void backToPause()
    {
        pauseSettingsManu.gameObject.SetActive(false);
        pauseMainMenu.gameObject.SetActive(true);
    }

    public void retryGame()
    {
        SceneManager.LoadScene("InGame");
    }
}
