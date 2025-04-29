using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameOver : NetworkBehaviour
{
    public RandomSceneLoader randomSceneLoader;

    public void PlayAgain()
    {
        if (isServer)
        {
            Time.timeScale = 1f;
            randomSceneLoader.LoadScene("Map1");
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
