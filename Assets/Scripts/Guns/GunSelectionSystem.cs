using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class GunSelectionSystem : MonoBehaviour
{
    public enum GunType
    {
        AR,
        SMG,
        Shotgun,
        Sniper
    }

    [Header("UI References")] 
    public Dropdown gunDropdown;

    public Dropdown perkDropdown;
    public Button startGameButton;

    [Header("Selected Options")] 
    public GunType selectedGun;

    public PerkSystem.PerkType selectedPerk;

    void Start()
    {
        gunDropdown.ClearOptions();
        gunDropdown.AddOptions(new System.Collections.Generic.List<string>
        {
            "AR", "SMG", "Shotgun", "Sniper"
        });

        perkDropdown.ClearOptions();
        perkDropdown.AddOptions(new System.Collections.Generic.List<string>
        {
            "x2 Damage", "/2 Damage", "Teleportation", "Aim Assist"
        });
        
        gunDropdown.onValueChanged.AddListener(delegate {UpdateGunSelection();});
        
        startGameButton.onClick.AddListener(StartGame);
    }

    void UpdateGunSelection()
    {
        selectedGun = (GunType)gunDropdown.value;
        Debug.Log($"Gun Selected: {selectedGun}");
    }

    void UpdatePerkSelection()
    {
        selectedPerk = (PerkSystem.PerkType)perkDropdown.value;
        Debug.Log($"Perk Selected: {selectedPerk}");
    }

    void StartGame()
    {
        Debug.Log($"Starting game with Gun: {selectedGun}, Perk: {selectedPerk}");


        //GameManager.Instance.SetPlayerLoadout(selectedGun, selectedPerk);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Games List");
    }
}
