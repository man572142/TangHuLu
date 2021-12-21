using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentScene;
    [SerializeField] int menuSceneIndex = 1;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
