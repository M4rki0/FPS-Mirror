using System.Collections.Generic;
using System.Linq;
using Mirror;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using QuickStart;

public class LobbyManager : NetworkManager
{
    public TMP_Text playerCountText;
    public Button startGameButton;
    private int maxPlayers = 2;
    public GameObject PlayerTextPrefab;
    
    // MAGICAL LIST OF PLAYERS
    public List<NetworkedLobbyPlayer> playerScripts;

    private int playerCount = 0;
    //public SyncList<string> playerNames = new SyncList<string>();

    void Start()
    {
        if (startGameButton)
        {
            //startGameButton.onClick.AddListener(OnStartGame);
            startGameButton.interactable = false;
        }
    }

    // Called when a player successfully connects
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        playerCount++;
        
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            GameObject joinedPlayer = Instantiate(PlayerTextPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            var lobbyPlayer = joinedPlayer.GetComponent<NetworkedLobbyPlayer>();
            Debug.Log("Trying to link ReadyUp nw lobbyplayer, data is " + lobbyPlayer);
            joinedPlayer.GetComponent<ReadyUp>().lobbyPlayer = lobbyPlayer;
            Debug.Log("Ready Up's player is now " + joinedPlayer.GetComponent<ReadyUp>().lobbyPlayer);
            
            lobbyPlayer.playerName = "Player " + playerCount;
            //lobbyPlayer.connectionId = conn.connectionId;
            NetworkServer.Spawn(joinedPlayer);

            joinedPlayer.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);

            lobbyPlayer.player = conn.identity.gameObject;
            playerScripts.Add(lobbyPlayer.player.GetComponent<NetworkedLobbyPlayer>());
            conn.identity.gameObject.GetComponent<PlayerScript>().SetReadyUpButtonPlayer(conn, joinedPlayer);
        }
    }
    
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("CLIENT CONNECTED");

        //var player = NetworkClient.connection.identity.gameObject;
        //var lobbyPlayer = GetLobbyPlayerByConnectionID((int)NetworkClient.connection.identity.netId);

        //lobbyPlayer.player = player;
    }
    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        Debug.Log("CLIENT DISCONNECTED");
    }
    
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (playerCount > 0)
        {
            playerCount--;
            //if (playerNames.Count > 0) playerNames.RemoveAt(playerNames.Count - 1);
        }

        base.OnServerDisconnect(conn);
        Debug.Log("PLAYER DISCONNECTED");
        //UpdatePlayerCount();
    }

    // Updates the UI with the current player count

    // Starts the game if enough players are present
    /*public void OnStartGame()
    {
        if (playerCount >= maxPlayers)
        {
            ServerChangeScene("GamesList");
        }
    }*/

    // Notifies clients when a new player joins
    /*[ClientRpc]
    void RpcNotifyPlayerJoined(string playerName)
    {
        Debug.Log($"{playerName} has joined the game!");
        Instantiate(PlayerText);
    }*/
    
    public static NetworkedLobbyPlayer GetLobbyPlayerByConnectionID(int connectionId)
    {
        var lobbyPlayers = FindObjectsByType<NetworkedLobbyPlayer>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        return lobbyPlayers.FirstOrDefault(lobbyPlayer => lobbyPlayer.connectionId == connectionId);
    }
}

