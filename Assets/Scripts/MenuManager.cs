using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // declaring variables
    public GameObject mainButtons;
    public GameObject settingsButtons;
    public GameObject howToPlayUi;
    public GameObject[] cutSceneImages;
    public GameObject cutscene;
    public CanvasGroup[] uiGroup;
    public bool fadedOut = false;
    public int i = 0;

    void Start()
    {
        // disables the settings part of the menu so it dosn't overlap with the main menu
        settingsButtons.SetActive(false);
        // disables the how to play part of the menu so it dosn't overlap with the main menu
        howToPlayUi.SetActive(false);
        cutscene.SetActive(false);
        foreach (GameObject Images in cutSceneImages)
        {
            Images.SetActive(false);
        }
    }

    // method for when the start button is pressed
    public void startGame()
    {
        // loads the scene with the main game inside
        SceneManager.LoadScene("InGame");
    }

    // method for when the quit button is pressed
    public void exitGame()
    {
        // exits the game
        Application.Quit();
    }

    //method for the settings part of the menu
    public void openSettings()
    {
        // disables the main menu ui and enables the settings part of the ui
        mainButtons.gameObject.SetActive(false);
        settingsButtons.gameObject.SetActive(true);
        howToPlayUi.SetActive(false);
    }

    // method for the fullscreen or windowed dropdown
    public void fullScreenChange()
    {
        // checks if the screen is not in fullscreen
        if(Screen.fullScreen == false)
        {
            // sets the screen to fullscreen
            Screen.fullScreen = true;
            // logs this to check if it works
            Debug.Log("went fullscreen");
        }
        // checks if the screen is in fullscreen
        else if(Screen.fullScreen == true)
        {
            // sets the game to windowed 
            Screen.fullScreen = false;
            // logs this to check if it works
            Debug.Log("went windowed");
        }
    }

    // method for going back to the main menu
    public void returnToMainMenu()
    {
        // disables the how to play and settings menu ui
        // enables the main menu ui
        settingsButtons.gameObject.SetActive(false);
        mainButtons.gameObject.SetActive(true);
        howToPlayUi.SetActive(false);
    }

    // method for when the player presses the how to play button
    public void howToPlayMenu()
    {
        // disables the main menu and the settings menu ui
        // enables the how to play menu ui
        mainButtons.gameObject.SetActive(false);
        settingsButtons.gameObject.SetActive(false);
        howToPlayUi.gameObject.SetActive(true);    
    }

    public void startCutscene()
    {
        mainButtons.SetActive(false);
        settingsButtons.SetActive(false);
        howToPlayUi.SetActive(false);
        cutscene.SetActive(true);
        playCutscene();
    }

    public void playCutscene()
    {
        StartCoroutine(cutsceneUpdate());
    }

    IEnumerator cutsceneUpdate()
    {
        foreach (GameObject scenes in cutSceneImages)
        {
            if (i <= 6)
            {
                scenes.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                fadedOut = true;
            }
            else
            {
                scenes.SetActive(true);
                yield return new WaitForSeconds(2.5f);
                fadedOut = true;
            }
        }

        startGame();
    }

    void Update()
    {
        if (fadedOut == true)
        {
            if (uiGroup[i].alpha < 1)
            {
                uiGroup[i].alpha += Time.deltaTime;
                if (uiGroup[i].alpha >= 1)
                {
                    fadedOut = false;
                    i++;
                }
            }
        }
    }
}
