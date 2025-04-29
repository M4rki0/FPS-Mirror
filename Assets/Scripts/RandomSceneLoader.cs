using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSceneLoader : NetworkBehaviour
{
    public string[] sceneNames; // Add scene names in the Inspector
    public bool canLoad;
    
    public void LoadRandomScene()
    {
        if (!canLoad) return;    
        Debug.Log("Client requesting scene change");
        CmdChangeSceneRandom();
    }

    public void LoadScene(string sceneName)
    {
        CmdChangeScene(sceneName);
    }

    [Command(requiresAuthority = false)]
    void CmdChangeSceneRandom()
    {
        if (sceneNames.Length == 0) return;
        string randomScene = sceneNames[Random.Range(0, sceneNames.Length)];
        //SceneManager.LoadScene(randomScene);
        Debug.Log("Attempting to change scene");
        FindAnyObjectByType<LobbyManager>().ServerChangeScene(randomScene);
    }
    
    [Command(requiresAuthority = false)]
    void CmdChangeScene(string sceneName)
    {
        //SceneManager.LoadScene(randomScene);
        Debug.Log("Attempting to change scene");
        FindAnyObjectByType<LobbyManager>().ServerChangeScene(sceneName);
    }
}

