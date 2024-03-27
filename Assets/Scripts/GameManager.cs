using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager {  get; private set; }

    [field: Header("Health")]
    public HealthSystem playerHealth = new HealthSystem(10, 10);

    [field: Header("Menus")]
    public GameObject gameOverScreen;
    public GameObject pauseMenu;
    public GameObject pauseMainMenu;
    public GameObject pauseSettingsManu;
    public GameObject exitCheck;
    public bool isPaused = false;
    public int levelTime = 300;
    public GameObject survivedMenu;
    public TextMeshProUGUI levelTimerUi;
    public GameObject ingameTimer;
    public GameObject laps;
    public GameObject battery;
    public GameObject[] batteries;

    public GameObject rounds;
    private bool gameOver = false;

    void Start()
    {
        StartCoroutine(timerCountdown());
    }

    void Awake()
    {
        // disables the game over screen and the pause screen 
        gameOverScreen.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        exitCheck.gameObject.SetActive(false);
        pauseSettingsManu.gameObject.SetActive(false);
        survivedMenu.gameObject.SetActive(false);
        rounds.gameObject.SetActive(true);
        ingameTimer.gameObject.SetActive(true);
        laps.gameObject.SetActive(true);
        battery.gameObject.SetActive(true);

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
        // checks if the player has died

        //pause Game
        // checks if the player is not in the game over screen and has pressed "p"
        if (Input.GetKeyDown(KeyCode.P) && !gameOver)
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            // enables the pause menu ui
            pauseMenu.gameObject.SetActive(true);
            rounds.gameObject.SetActive(false);
            ingameTimer.gameObject.SetActive(false);
            laps.gameObject.SetActive(false);
            battery.gameObject.SetActive(false);

            //sets time to pause
            Time.timeScale = 0f;
        }

        if (!isPaused)
        {
            pauseMenu.gameObject.SetActive(false);
            rounds.gameObject.SetActive(true);
            isPaused = false;
            Time.timeScale = 1f;
            ingameTimer.gameObject.SetActive(true);
            laps.gameObject.SetActive(true);
            battery.gameObject.SetActive(true);
        }

        if (levelTime <= 0)
        {
            survivedScreen();
        }

        if (playerHealth.Health == 10)
        {
            batteries[0].SetActive(true);
            batteries[1].SetActive(false);
            batteries[2].SetActive(false);
            batteries[3].SetActive(false);
            batteries[4].SetActive(false);
            batteries[5].SetActive(false);

        }
        if (playerHealth.Health <= 8)
        {
            batteries[0].SetActive(false);
            batteries[1].SetActive(true);
            batteries[2].SetActive(false);
            batteries[3].SetActive(false);
            batteries[4].SetActive(false);
            batteries[5].SetActive(false);
        }
        if (playerHealth.Health <= 6)
        {
            batteries[0].SetActive(false);
            batteries[1].SetActive(false);
            batteries[2].SetActive(true);
            batteries[3].SetActive(false);
            batteries[4].SetActive(false);
            batteries[5].SetActive(false);
        }
        if (playerHealth.Health <= 4)
        {
            batteries[0].SetActive(false);
            batteries[1].SetActive(false);
            batteries[2].SetActive(false);
            batteries[3].SetActive(true);
            batteries[4].SetActive(false);
            batteries[5].SetActive(false);
        }
        if (playerHealth.Health <= 2)
        {
            batteries[0].SetActive(false);
            batteries[1].SetActive(false);
            batteries[2].SetActive(false);
            batteries[3].SetActive(false);
            batteries[4].SetActive(true);
            batteries[5].SetActive(false);
        }
        if (playerHealth.Health <= 0)
        {
            batteries[0].SetActive(false);
            batteries[1].SetActive(false);
            batteries[2].SetActive(false);
            batteries[3].SetActive(false);
            batteries[4].SetActive(false);
            batteries[5].SetActive(true);
        }
    }

    public void gameOverded()
    {
        // enables the game over screen ui
        gameOverScreen.gameObject.SetActive(true);
        // sets time to pause
        Time.timeScale = 0f;
        gameOver = true;
    }

    public void survivedScreen()
    {
        Time.timeScale = 0f;
        survivedMenu.gameObject.SetActive(true);
    }

    // method for unpausing
    public void resumeGame()
    {
        // disables the pause menu
        pauseMenu.gameObject.SetActive(false);
         //sets the isPaused bool to false
        isPaused = false;
         //returns time to normal
        Time.timeScale = 1f;
    }

    // method for opening the settings menu
    public void openSettingsInPause()
    {
        // disables the main pause menu ui 
        // enables the settings part of the pause menu ui
        pauseMainMenu.gameObject.SetActive(false);
        pauseSettingsManu.gameObject.SetActive(true);
    }

    // method for asking the player if theyre sure they want to go to the main menu
    public void exitToMenuCheck()
    {
        // diables the pause menu ui
        // enables the ui for asking the player if they are sure about returning to the main menu
        pauseMainMenu.gameObject.SetActive(false);
        exitCheck.gameObject.SetActive(true);
    }

    // method for the player confirming they want to go back to the main menu
    public void exitToMenuYes()
    {
        // changes the scene to the main menu scene
        SceneManager.LoadScene("MenuScene");
    }

    // method to go back to the pause menu
    public void exitToMenuNo()
    {
        // disables the check for returning to the main menu
        // enables the pause menu ui
        exitCheck.gameObject.SetActive(false);
        pauseMainMenu.gameObject.SetActive(true);
    }

    // method for changing the screen in game
    public void fullScreenChange()
    {
        // checks if the screen is not full
        if (Screen.fullScreen == false)
        {
            // sets the screen to full
            Screen.fullScreen = true;
            // logs to check this is working
            Debug.Log("went fullscreen");
        }
        // checks if the screen is full
        else if (Screen.fullScreen == true)
        {
            // sets the screen to windowed
            Screen.fullScreen = false;
            // logs to check this is working
            Debug.Log("went windowed");
        }
    }

    // method for going back to the pause menu from the settings menu
    public void backToPause()
    {
        // disables the settings menu
        // enables the main pause menu ui
        pauseSettingsManu.gameObject.SetActive(false);
        pauseMainMenu.gameObject.SetActive(true);
    }

    // method for the game over screen
    public void retryGame()
    {
        // reloads the scene if the player chooses to retry the level
        SceneManager.LoadScene("InGame");
    }

    IEnumerator timerCountdown()
    {
        while (levelTime > 0)
        {
            if (gameOver == false)
            {
                levelTimerUi.text = levelTime.ToString();
                yield return new WaitForSeconds(1f);
                levelTime--;
            }
        }
    }
}
