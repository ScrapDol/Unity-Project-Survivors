using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
