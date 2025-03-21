using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public GameObject arPrefab;
    public GameObject smgPrefab;
    public GameObject sniperPrefab;
    public GameObject shotgunPrefab;

    private void Start()
    {
        // Get the selected gun from the persistent GameManager
        var selectedGun = GameManager.Instance.GetSelectedGun();
        GameObject gunToSpawn = GetGunPrefab(selectedGun);

        if (gunToSpawn != null)
        {
            Instantiate(gunToSpawn, transform.position, transform.rotation, transform);
        }
    }

    private GameObject GetGunPrefab(GunSelectionSystem.GunType gunType)
    {
        switch (gunType)
        {
            case GunSelectionSystem.GunType.AR: return arPrefab;
            case GunSelectionSystem.GunType.SMG: return smgPrefab;
            case GunSelectionSystem.GunType.Sniper: return sniperPrefab;
            case GunSelectionSystem.GunType.Shotgun: return shotgunPrefab;
            default: return null;
        }
    }
}