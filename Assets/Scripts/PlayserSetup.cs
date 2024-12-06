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
        var selectedGun = GameManager1.Instance.GetSelectedGun();
        var playerPerk = GameManager1.Instance.GetSelectedPerk();

        // Spawn the selected gun
        GameObject gunToSpawn = null;
        switch (selectedGun)
        {
            case GunSelectionWithButtons.GunType.AR: gunToSpawn = arPrefab; break;
            case GunSelectionWithButtons.GunType.SMG: gunToSpawn = smgPrefab; break;
            case GunSelectionWithButtons.GunType.Sniper: gunToSpawn = sniperPrefab; break;
            case GunSelectionWithButtons.GunType.Shotgun: gunToSpawn = shotgunPrefab; break;
        }

        if (gunToSpawn != null)
        {
            Instantiate(gunToSpawn, transform.position, transform.rotation, transform);
        }
    }
}

