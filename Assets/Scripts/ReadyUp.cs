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
        if(lobbyPlayer) lobbyPlayer.ReadyUp();
    }

    public void ServerReadyUp()
    {
        if (!lobbyPlayer) return;
        
        if(buttonText) buttonText.text = lobbyPlayer.isReady ? "Ready" : "Not Ready";

        Debug.Log("Player is " + (lobbyPlayer.isReady ? "Ready" : "Not Ready"));
    }
}
