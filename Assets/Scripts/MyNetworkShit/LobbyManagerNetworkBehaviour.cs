using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LobbyManagerNetworkBehaviour : NetworkBehaviour
{
    public TMP_Text playerCountText;
    public GameObject PlayerText;
    [SyncVar] private int playerCount = 0;
    public SyncList<string> playerNames = new SyncList<string>();
    public Button startGameButton;
    
    /*public void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        playerNames.Add(playerName);

        UpdatePlayerCount();
        RpcNotifyPlayerJoined(playerName);
    }*/
    
    /*public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        playerCount++;
        string playerName = $"Player {playerCount}";
        playerNames.Add(playerName);

        UpdatePlayerCount();
        RpcNotifyPlayerJoined(playerName);
    }*/
    
    [ClientRpc]
    void RpcNotifyPlayerJoined(string playerName)
    {
        Debug.Log($"{playerName} has joined the game!");
        Instantiate(PlayerText);
    }

    /*private void UpdatePlayerCount()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            playerCountText.text = $"Players in Lobby: {playerCount}/{maxPlayers}";
            startGameButton.interactable = playerCount >= maxPlayers;
        }
    }*/
}
