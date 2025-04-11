using UnityEngine;
using Mirror;

public class Weapon : NetworkBehaviour
{
    public int damage = 25;
    public float range = 100f;
    public Camera fpsCam;
    public float cooldown;
    public int weaponAmmo;

    [SerializeField] private LayerMask hitMask;
    public GameObject bullet;
    public GameObject firePosition;
    public float bulletSpeed;

    void Start()
    {
        fpsCam = Camera.main;
    }
    
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
