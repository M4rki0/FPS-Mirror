using System.Collections;
using System.Collections.Generic;
using Mirror;
using QuickStart;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGamesList : NetworkBehaviour
{

    public void GoScene()
    {
        if (isServer)
        {
            FindAnyObjectByType<LobbyManager>().ServerChangeScene("GamesList");
        }
        //SceneManager.LoadScene("GamesList");
    }
}
