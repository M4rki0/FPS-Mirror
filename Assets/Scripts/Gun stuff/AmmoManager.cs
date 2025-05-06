using System.Collections;
using System.Collections.Generic;
using QuickStart;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    //public List<WeaponData> availableWeapons; // List of weapons the player has
    private int currentWeaponIndex = 0; // Track currently selected weapon
    public WeaponData currentWeapon;
    public int currentAmmo;
    private TMP_Text ammoText;
    private float nextFireTime = 0f;
    public bool isReloading = false;
    private bool canHoldFire => currentWeapon.allowHoldFire;
    [SerializeField] private PlayerScript playerScript;
    private Animator animator;

    public void EquipWeapon()
    {
        var ammoTextGO = GameObject.FindWithTag("AmmoText");
        if (ammoTextGO && !ammoText)
        {
            Debug.Log("SETTING AMMO TEXT");
            ammoText = ammoTextGO.GetComponent<TMP_Text>();
        }
        UpdateUI();
    }

    void Update()
    {
        if (isReloading) return;

        // Shooting Logic
        /*if ((canHoldFire && Input.GetButton("Fire1")) || (!canHoldFire && Input.GetButton("Fire1") && isReloading == false))
        {
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + currentWeapon.fireRate;
                Shoot();
            }
            
        }*/

        // Reload Logic
        if (Input.GetKeyDown(KeyCode.R) || currentAmmo == 0) 
        {
            StartCoroutine(Reload());
            isReloading = true;
        }

        // Weapon Switching
        //HandleWeaponSwitching();

        if (currentAmmo < 0) isReloading = false;
    }

    public void Shoot()
    {
        // if we can't shoot then dont fucking shoot
        if (!playerScript.isPlayerInGame) return;
        
        if (currentAmmo > 0)
        {
            currentAmmo--;
            Debug.Log($"Shot fired! Ammo left: {currentAmmo}");
            UpdateUI();
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    void UpdateUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Ammo: {currentAmmo}/{currentWeapon.maxAmmo}";
        }
    }

    IEnumerator Reload()
    {
        //playerScript.canShoot = false;
        isReloading = true;
        animator.SetBool("isReloading", true);
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(currentWeapon.reloadSpeed);
        currentAmmo = currentWeapon.maxAmmo;
        UpdateUI();
        isReloading = false;
        Debug.Log("Gun has been reloaded");
    }
    

    /*void HandleWeaponSwitching()
    {
        // Scroll Wheel Switching
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SwitchWeapon(1);
        }
        else if (scroll < 0f)
        {
            SwitchWeapon(-1);
        }

        // Number Key Switching (1, 2, 3, ...)
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeapon(i - currentWeaponIndex);
            }
        }
    }

    void SwitchWeapon(int change)
    {
        if (availableWeapons.Count <= 1) return;

        currentWeaponIndex = (currentWeaponIndex + change) % availableWeapons.Count;
        if (currentWeaponIndex < 0) currentWeaponIndex += availableWeapons.Count; // Wrap around if negative

        currentWeapon = availableWeapons[currentWeaponIndex];
        currentAmmo = currentWeapon.maxAmmo;
        nextFireTime = 0f;
        isReloading = false;
        UpdateUI();

        Debug.Log($"Switched to {currentWeapon.weaponName}");
    }*/
}
