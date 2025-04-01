using UnityEngine;
using Mirror;

public class Weapon : NetworkBehaviour
{
    public float damage = 25f;
    public float range = 100f;
    public Camera fpsCam;
    public float weaponCooldown;
    public float weaponAmmo;

    [SerializeField] private LayerMask hitMask;
    public Object weaponBullet;
    public object weaponFirePosition;

    [Command]
    private void CmdShoot()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range, hitMask))
        {
            if (hit.collider.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
