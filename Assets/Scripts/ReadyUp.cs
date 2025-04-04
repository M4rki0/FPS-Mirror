using UnityEngine;
using TMPro;

public class ReadyUp : MonoBehaviour
{
    public NetworkedLobbyPlayer lobbyPlayer;
    public TMP_Text buttonText;

    public void Start()
    {
        buttonText.text = "Not Ready";
    }

    public void ReadyUpButton()
    {
        lobbyPlayer.ReadyUp();
    }

    public void ServerReadyUp()
    {
        buttonText.text = lobbyPlayer.isReady ? "Ready" : "Not Ready";

        Debug.Log("Player is " + (lobbyPlayer.isReady ? "Ready" : "Not Ready"));
    }
}
