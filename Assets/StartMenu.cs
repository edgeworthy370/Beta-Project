using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string GameSceneName = "2D Mover 1";

    public void StartGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
