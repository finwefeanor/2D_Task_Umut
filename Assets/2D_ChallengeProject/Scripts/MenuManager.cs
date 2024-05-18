using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    void Awake()
    {
        Time.timeScale = 1.0f;  // Ensure the time scale is reset when the main menu loads.
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Debug.Log("Exit game requested."); 
        Application.Quit();
    }
}
