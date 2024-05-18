using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Replace "GameScene" with the name of your actual game scene
    }
    

    public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // This will only work in the Unity Editor
#endif
    }
}
