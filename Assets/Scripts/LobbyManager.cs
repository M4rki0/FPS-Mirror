using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : NetworkManager
{
    public Text playerCountText;
    public Button startGameButton;
    private int maxPlayers = 2;
    private GameObject players;

    void Start()
    {
        // Button click listener to start the game
        if (startGameButton)
        {
            startGameButton.onClick.AddListener(OnStartGame);
            startGameButton.interactable = false;
        }
    }

    // Called when a player successfully connects to the lobby
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        // Update the player count in the UI
        UpdatePlayerCount();
    }

    public override void OnClientConnect()
    {
     base.OnClientConnect();
     Debug.Log("CLIENT CONNECTED");
    }

    // Update the number of players in the lobby
    private void UpdatePlayerCount()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            int playerCount = NetworkServer.connections.Count; 
            players = GameObject.FindWithTag("Players").gameObject; 
                //= $"Players in Lobby: {playerCount}/{maxPlayers}";
            // Enable the start button when the max player count is reached
            //startGameButton.interactable = playerCount >= maxPlayers;
        }
    }

    // Handle starting the game (move to the next scene)
    public void OnStartGame()
    {
        if (NetworkServer.connections.Count >= maxPlayers)
        {
            ServerChangeScene("GamesList");
        }
    }

    // Override to clean up when players disconnect
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        UpdatePlayerCount();
    }
}