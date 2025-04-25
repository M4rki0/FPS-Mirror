using Mirror;
using QuickStart;
using UnityEngine;

public class Respawn : NetworkBehaviour
{
    private PlayerHealth health;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
    }

    public void RequestRespawn() // Called by UI button
    {
        if (isLocalPlayer)
        {
            CmdRequestRespawn();
        }
    }

    [Command]
    private void CmdRequestRespawn()
    {
        // Reset player stats
        health.ResetHealth();

        // Move to start position
        Transform startPos = NetworkManager.singleton.GetStartPosition();
        if (startPos != null)
        {
            transform.position = startPos.position;
        }

        RpcRespawnClient();
    }

    [ClientRpc]
    private void RpcRespawnClient()
    {
        if (!isLocalPlayer) return;

        // Re-enable controls
        GetComponent<PlayerScript>().enabled = true;
        GetComponent<CharacterController>().enabled = true;

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = true;
        }

        // Hide respawn button
        GameObject respawnBtn = GameObject.FindWithTag("RespawnButton");
        if (respawnBtn)
        {
            respawnBtn.SetActive(false);
        }
    }
}