using Mirror;
using QuickStart;
using UnityEngine;

public class Respawn : NetworkBehaviour
{
    private PlayerHealth health;
    private PlayerScript _playerScript;

    public void RequestRespawn() // Called by UI button
    {
        _playerScript = PlayerScript.localPlayer;
        if (_playerScript.isLocalPlayer)
        {
            CmdRequestRespawn(_playerScript.gameObject);
            Debug.Log("Player is trying to respawn");
        }
    }
    
    [Command(requiresAuthority = false)]
    private void CmdRequestRespawn(GameObject player)
    {
        // Reset player stats
        var hp = player.GetComponent<PlayerHealth>();
        hp.ResetHealth();

        // Move to start position
        Transform startPos = NetworkManager.singleton.GetStartPosition();
        if (startPos != null)
        {
            player.transform.position = startPos.position;
        }

        var identity = player.GetComponent<NetworkIdentity>();

        RpcRespawnClient(identity.connectionToClient, player);
    }

    [TargetRpc]
    private void RpcRespawnClient(NetworkConnectionToClient playerConn, GameObject player)
    {
        var playerScript = player.GetComponent<PlayerScript>();
        
        Debug.Log("conn: "+playerConn+" script: "+playerScript);
        // Re-enable controls
        player.GetComponent<PlayerScript>().enabled = true;
        playerScript.GetComponent<CharacterController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponentInChildren<MouseLook>().enabled = true;

        foreach (var rend in playerScript.GetComponentsInChildren<Renderer>())
        {
            rend.enabled = true;
        }

        // Hide respawn button
        GameObject respawnBtn = GameObject.FindWithTag("RespawnButton");
        if (respawnBtn)
        {
            respawnBtn.GetComponent<Canvas>().enabled = false;
        }
    }
}