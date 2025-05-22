using System;
using System.Collections;
using System.Collections.Generic;
using QuickStart;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Canvas pauseCanvas;
    public PlayerScript player;
    
    public void ResumeGame()
    {
        Debug.Log(player);
        player.CloseEscapeMenu();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
