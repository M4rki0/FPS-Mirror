using Mirror;
using QuickStart;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameObject localPlayer;
    
    [Header("Player Loadout")] 
    public GunSelectionSystem.GunType selectedGun;
    public PerkSystem.PerkType selectedPerk;
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


    // Method to retrieve the selected gun (if needed)
    public GunSelectionSystem.GunType GetSelectedGun()
    {
        GameObject.FindWithTag("Guns");
        return selectedGun;
    }

    // Method to retrieve the selected perk (if needed)
    public PerkSystem.PerkType GetSelectedPerk()
    {
        GameObject.FindWithTag("Perks");
        return selectedPerk;
    }
}
