using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSceneLoader : MonoBehaviour
{
    public string[] sceneNames; // Add scene names in the Inspector

    public void LoadRandomScene()
    {
        if (sceneNames.Length == 0) return;
        string randomScene = sceneNames[Random.Range(0, sceneNames.Length)];
        SceneManager.LoadScene(randomScene);
    }
}

