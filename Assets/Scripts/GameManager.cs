using System;
using System.Collections.Generic;
using Mirror;
using QuickStart;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : NetworkBehaviour
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

    [Header("Match Timer")] [SyncVar] public float matchTimeRemaining = 300f;
    public TMP_Text matchTimerText;
    public GameObject gameOverPanel;

    public bool matchRunning = false;

    public GameObject hudCanvas;

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

    private void Start()
    {
        SceneManager.sceneLoaded += (scene, loadSceneMode) =>
        {
            if (scene.name != "MatchMaking" && scene.name != "Lobby")
            {
                FindMatchUI();
            }
        };

        if (gameOverPanel == null) return;
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        HandleMatchTimer();
    }

    private void HandleMatchTimer()
    {
        if (!matchRunning) return;

        if (isServer)
        {
            if (matchTimeRemaining > 0)
            {
                matchTimeRemaining -= Time.deltaTime;
            }
            else
            {
                {
                    matchTimeRemaining = 0;
                    matchRunning = false;
                    RpcMatchOver();
                }
            }
        }

        if (localPlayer != null)
        {
            UpdateMatchTimerUI(matchTimeRemaining);
        }
    }

    private void UpdateMatchTimerUI(float timeToDisplay)
    {
        if (matchTimerText == null) return;

        timeToDisplay += 1f;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        matchTimerText.text = String.Format("{0:00}:{1:00}", minutes, seconds); 
    }

    [ClientRpc]
    private void RpcMatchOver()
    {
        hudCanvas.SetActive(false);
        Debug.Log("Match Over! Showing Game Over Panel");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartMatch()
    {
        if (!isServer) return;

        matchTimeRemaining = 10f;
        matchRunning = true;
        //FindMatchUI();
    }

    /*public void PlayAgain()
    {
        if (isServer)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Map1");
        }
    }
    
    public void MainMenu()
    {
        if (isServer)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }
    }*/

    /*private void FindMatchUI()
    {
        if (matchTimerText == null) return;
        matchTimerText = GameObject.FindWithTag("MatchTimerText")?.GetComponent<TMP_Text>();

        if (gameOverPanel == null) return;
        gameOverPanel = GameObject.FindWithTag("GameOverPanel");
    }*/
    
    private void FindMatchUI()
    {
        if (matchTimerText == null)
        {
            matchTimerText = GameObject.FindWithTag("MatchTimerText")?.GetComponent<TMP_Text>();
            if (matchTimerText == null)
                Debug.LogWarning("Match Timer Text not found!");
        }
        
        if (hudCanvas == null)
        {
            hudCanvas = GameObject.FindWithTag("HUDCanvas");
            if (hudCanvas == null)
                Debug.LogWarning("HUD Canvas not found");
            else
                hudCanvas.SetActive(true);
        }

        if (gameOverPanel == null)
        {
            gameOverPanel = GameObject.FindWithTag("GameOverPanel");
            if (gameOverPanel == null)
                Debug.LogWarning("Game Over Panel not found!");
            else
            {
                gameOverPanel.SetActive(false);
            }
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
        StartMatch();
        Debug.Log("Final check passed, can load scene");
        randSceneLoad.canLoad = true;
    }

    // same with this - check if ready instead
    private bool ReadyToEnter(ReadyUp readyUp)
    {
        return readyUp.lobbyPlayer.isReady;
    }

    void RestartTimer()
    {
        
    }
}
