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
            NetworkManager.singleton.ServerChangeScene("GamesList");
        }
        //SceneManager.LoadScene("GamesList");
    }
}
