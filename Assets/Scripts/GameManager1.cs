using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance { get; private set; }

    [Header("Player Loadout")]
    public GunSelectionWithButtons.GunType selectedGun;
    public PerkSystem.PerkType selectedPerk;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to set the player's loadout
    public void SetPlayerLoadout(GunSelectionWithButtons.GunType gun, PerkSystem.PerkType perk)
    {
        selectedGun = gun;
        selectedPerk = perk;
        Debug.Log($"Loadout set: Gun = {gun}, Perk = {perk}");
    }

    // Method to retrieve the selected gun (if needed)
    public GunSelectionWithButtons.GunType GetSelectedGun()
    {
        return selectedGun;
    }

    // Method to retrieve the selected perk (if needed)
    public PerkSystem.PerkType GetSelectedPerk()
    {
        return selectedPerk;
    }
    
    public void ResetLoadout()
    {
        selectedGun = GunSelectionWithButtons.GunType.AR;
        selectedPerk = PerkSystem.PerkType.DoubleDamage;
    }

}