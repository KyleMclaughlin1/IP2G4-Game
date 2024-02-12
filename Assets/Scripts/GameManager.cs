using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager {  get; private set; }
   

    public HealthSystem playerHealth = new HealthSystem(10, 10);

    [field: Header("Menus")]
    public GameObject gameOverScreen;
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Awake()
    {
        gameOverScreen.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.gameObject.SetActive(!isPaused);
            isPaused = !isPaused;
            Time.timeScale = 0f;
        }
    }

}
