using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject gameMenuUI;

    void Start()
    {

        pauseMenuUI.SetActive(false);
        gameMenuUI.SetActive(true);
        Time.timeScale = 1f;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameMenuUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameMenuUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
