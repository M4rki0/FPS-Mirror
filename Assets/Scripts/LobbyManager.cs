using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : NetworkManager
{
    public Text playerCountText;
    public Button startGameButton;
    private int maxPlayers = 2;

    void Start()
    {
        // Button click listener to start the game
        startGameButton.onClick.AddListener(OnStartGame);
        startGameButton.interactable = false;
    }

    // Called when a player successfully connects to the lobby
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        // Update the player count in the UI
        UpdatePlayerCount();
    }

    // Update the number of players in the lobby
    private void UpdatePlayerCount()
    {
        int playerCount = NetworkServer.connections.Count;
        playerCountText.text = $"Players in Lobby: {playerCount}/{maxPlayers}";

        // Enable the start button when the max player count is reached
        startGameButton.interactable = playerCount >= maxPlayers;
    }

    // Handle starting the game (move to the next scene)
    private void OnStartGame()
    {
        if (NetworkServer.connections.Count >= maxPlayers)
        {
            ServerChangeScene("GameScene"); // Assuming GameScene is the match scene
        }
    }

    // Override to clean up when players disconnect
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        UpdatePlayerCount();
    }
}