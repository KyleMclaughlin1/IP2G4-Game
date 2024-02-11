using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject mainButtons;
    public GameObject settingsButtons;
    public GameObject howToPlayUi;

    void Start()
    {
        settingsButtons.SetActive(false);
        howToPlayUi.SetActive(false);
    }

    public void startGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void openSettings()
    {
        mainButtons.gameObject.SetActive(false);
        settingsButtons.gameObject.SetActive(true);
        howToPlayUi.SetActive(false);
    }

    public void fullScreenChange()
    {
        if(Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
            Debug.Log("went fullscreen");
        }
        else if(Screen.fullScreen == true)
        {
            Screen.fullScreen = false;
            Debug.Log("went windowed");
        }
    }

    public void returnToMainMenu()
    {
        settingsButtons.gameObject.SetActive(false);
        mainButtons.gameObject.SetActive(true);
        howToPlayUi.SetActive(false);
    }

    public void howToPlayMenu()
    {
        mainButtons.gameObject.SetActive(false);
        settingsButtons.gameObject.SetActive(false);
        howToPlayUi.gameObject.SetActive(true);    
    }
}
