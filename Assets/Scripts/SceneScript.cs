using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuickStart
{
    public class SceneScript : NetworkBehaviour
    {
        public PlayerScript playerScript;
        public SceneReference sceneReference;
        
        private void Start()
        {
            sceneReference = GetComponent<SceneReference>();
            
        }
        public void ButtonSendMessage()
        {
            if (playerScript != null)  
                playerScript.CmdSendPlayerMessage();
        }
        
        public void ButtonChangeScene()
        {
            if (isServer)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "MyScene")
                    NetworkManager.singleton.ServerChangeScene("MyOtherScene");
                else
                    NetworkManager.singleton.ServerChangeScene("MyScene");
            }
            else
                Debug.Log("You are not Host.");
        }
    }
}
