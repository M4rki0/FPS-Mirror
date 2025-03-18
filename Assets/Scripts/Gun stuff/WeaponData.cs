using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int maxAmmo;
    public float fireRate;
    public float reloadSpeed;
    public bool allowHoldFire;
}