using System.Collections.Generic;
using Mirror;
using QuickStart;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameObject localPlayer;

    public RandomSceneLoader randSceneLoad;
    
    [Header("Player Loadout")] 
    public GunSelectionSystem.GunType selectedGun;
    public PerkSystem.PerkType selectedPerk;
    public GameObject ARButton;
    public GameObject SMGButton;
    public GameObject SniperButton;
    public GameObject ShotgunButton;
    public ReadyUp readyUp;

    public List<ReadyUp> readyUps;

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

    public void TryStartGame()
    {
        // this all doesnt work as the bool gets reset when player instantiated in level
        // can reuse to check if players are ready though
        if (!readyUps.TrueForAll(ReadyToEnter)) return;
        Debug.Log("Final check passed, can load scene");
        randSceneLoad.canLoad = true;
    }

    // same with this - check if ready instead
    private bool ReadyToEnter(ReadyUp readyUp)
    {
        return readyUp.lobbyPlayer.isReady;
    }
}
