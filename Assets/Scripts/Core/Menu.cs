using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    string currentSceneName;

    private void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Loads Scenes
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Restarts level
    public void restartLevel()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    // Exits Game
    public void Exit()
    {
        Application.Quit();
    }

}
