using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class MatchmakingManager : MonoBehaviour
{
    public Button createLobbyButton;
    public Button joinLobbyButton;

    private void Start()
    {
        createLobbyButton.onClick.AddListener(CreateLobby);
        joinLobbyButton.onClick.AddListener(JoinLobby);
    }

    // Creating a new lobby (starts a server)
    public void CreateLobby()
    {
        NetworkManager.singleton.StartHost();
        Debug.Log("Created Lobby: Waiting for players...");
    }

    // Joining an existing lobby
    public void JoinLobby()
    {
        string lobbyAddress = "10.16.39.17"; // Example address, use real matchmaking data
        NetworkManager.singleton.networkAddress = lobbyAddress;
        NetworkManager.singleton.StartClient();
        Debug.Log("Joining Lobby...");
    }
}