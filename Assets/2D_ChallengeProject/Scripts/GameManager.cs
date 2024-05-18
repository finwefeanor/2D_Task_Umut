using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameIsPaused = true;
    public GameObject inGameMenuPanel;  

    private void Start()
    {
        //PauseGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else if(GameIsPaused == false)
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        inGameMenuPanel.SetActive(false);  // Hide the main menu panel
        GameIsPaused = false;
        Time.timeScale = 1.0f;  // Resume the game time.
    }

    public void PauseGame()
    {
        inGameMenuPanel.SetActive(true);  // Show the main menu panel
        GameIsPaused = true;
        Time.timeScale = 0.0f;  // Pause the game time.
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
        //Application.Quit();
    }
}
