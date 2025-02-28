using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public GameObject arPrefab;
    public GameObject smgPrefab;
    public GameObject sniperPrefab;
    public GameObject shotgunPrefab;

    private void Start()
    {
        // Get the selected gun from the GameManager
        var selectedGun = GameManager.Instance.GetSelectedGun();

        // Spawn the selected gun
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

