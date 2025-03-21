using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Loadout")]
    public GunSelectionSystem.GunType selectedGun;
    public GunSelectionSystem.PerkType selectedPerk;
    public GameObject ARButton;
    public GameObject SMGButton;
    public GameObject SniperButton;
    public GameObject ShotgunButton;

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
    public void SetPlayerLoadout(GunSelectionSystem.GunType gun, GunSelectionSystem.PerkType perk)
    {
        selectedGun = gun;
        selectedPerk = perk;
        Debug.Log($"Loadout set: Gun = {gun}, Perk = {perk}");
    }

    // Method to retrieve the selected gun (if needed)
    public GunSelectionSystem.GunType GetSelectedGun()
    {
        GameObject.FindWithTag("Guns");
        return selectedGun;
    }

    // Method to retrieve the selected perk (if needed)
    public GunSelectionSystem.PerkType GetSelectedPerk()
    {
        GameObject.FindWithTag("Perks");
        return selectedPerk;
    }
}
