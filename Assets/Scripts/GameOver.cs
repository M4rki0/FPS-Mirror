using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameOver : NetworkBehaviour
{
    public RandomSceneLoader randomSceneLoader;
    public GameManager gameManager;

    public void PlayAgain()
    {
        if (isServer)
        {
            Time.timeScale = 1f;
            randomSceneLoader.LoadScene("Map1");
            gameManager.RestartTimer();
        }
    }
    
    public void MainMenu()
    {
        if (isServer)
        {
            Time.timeScale = 1f;
            randomSceneLoader.LoadScene("Menu");
        }
    }
}
