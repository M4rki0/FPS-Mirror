using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SyncVar] public GunSelectionSystem.GunType selectedGun;

    public GameObject arPrefab;
    public GameObject smgPrefab;
    public GameObject sniperPrefab;
    public GameObject shotgunPrefab;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        
        // Tell the server what gun this player should have
        CmdSetGun(GameManager.Instance.GetSelectedGun());
    }

    [Command]
    void CmdSetGun(GunSelectionSystem.GunType gun)
    {
        selectedGun = gun; // Sync the gun across the network
        RpcSetupGun(); // Update the gun for all clients
    }

    [ClientRpc]
    void RpcSetupGun()
    {
        SetupGun();
    }

    void SetupGun()
    {
        GameObject gunToSpawn = null;
        switch (selectedGun)
        {
            case GunSelectionSystem.GunType.AR: gunToSpawn = arPrefab; break;
            case GunSelectionSystem.GunType.SMG: gunToSpawn = smgPrefab; break;
            case GunSelectionSystem.GunType.Sniper: gunToSpawn = sniperPrefab; break;
            case GunSelectionSystem.GunType.Shotgun: gunToSpawn = shotgunPrefab; break;
        }

        if (gunToSpawn != null)
        {
            Instantiate(gunToSpawn, transform.position, transform.rotation, transform);
        }
    }
}