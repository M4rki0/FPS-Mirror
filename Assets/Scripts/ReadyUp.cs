using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class ReadyUp : MonoBehaviour
{
    public NetworkedLobbyPlayer lobbyPlayer;
    public TMP_Text buttonText;
    public Button readyButton;

    [SerializeField] private Sprite readyup, readydown;

    public void Start()
    {
        buttonText.text = "Not Ready";
    }

    public void ReadyUpButton()
    {
        if(lobbyPlayer) lobbyPlayer.ReadyUp();
    }

    /*public void ServerReadyUp()
    {
        if (!lobbyPlayer) return;
        
        if (lobbyPlayer.isReady)
        {
            readyButton.GetComponent<Image>().sprite = readyup;
        }
        else
        {
            readyButton.GetComponent<Image>().sprite = readydown;
        }

        Debug.Log("Player is " + (lobbyPlayer.isReady ? "Ready" : "Not Ready"));
    }*/
}
