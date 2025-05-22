using Mirror;
using QuickStart;
using TMPro;
using UnityEngine;

public class NetworkedLobbyPlayer : NetworkBehaviour
{
    [SyncVar(hook = "OnPlayerNameChanged")]
    public string playerName;

    [SyncVar] public int connectionId;
    [SyncVar] public GameObject player;

    [SyncVar(hook = "OnIsReadyChanged")]
    public bool isReady;

    public TMP_Text nameText;
    public TMP_Text readyText;

    private void Start()
    {
        var container = GameObject.FindWithTag("PlayerNameLobbyContainer").transform;
        transform.SetParent(container, false);

        NetworkIdentity identity = GetComponent<NetworkIdentity>();
        if (identity.isLocalPlayer)
        {
            /*var readyUpButton = FindAnyObjectByType<ReadyUp>();
            if (!readyUpButton)
            {
                Debug.LogWarning("Could not find ready up button");
            }
            readyUpButton.lobbyPlayer = this;*/

            readyText.text = "(Not Ready)";
            readyText.color  = Color.red;
        }
    }

    void OnPlayerNameChanged(string oldName, string newName)
    {
        nameText.text = newName;
    }
    void OnIsReadyChanged(bool oldIsReady, bool newIsReady)
    {
        if (newIsReady)
        {
            readyText.text = "(Ready)";
            readyText.color = Color.green;
        }
        else
        {
            readyText.text = "(Not Ready)";
            readyText.color  = Color.red;
        }

        Debug.Log("Ready Up: "+isReady);
    }

    [Command]
    public void ReadyUp()
    {
        isReady = !isReady;
    }
}
