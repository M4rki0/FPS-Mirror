using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Mirror.Examples.BenchmarkIdle;
using QuickStart;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerHealth : NetworkBehaviour
{
    public int maxHealth = 100;
    
    [SyncVar(hook = nameof(OnHealthChanged))]
    private int currentHealth;

    private TMP_Text healthText;

    private int damage = 5;

    private PlayerScript _playerScript;

    private void Start()
    {
        _playerScript = GetComponent<PlayerScript>();
        if (isServer)
        {
            currentHealth = maxHealth;
        }
    }

    void Update()
    {
        var healthTextGO = GameObject.FindWithTag("HealthText");
        if (healthTextGO && !healthText)
        {
            Debug.Log("SETTING HEALTH");
            healthText = healthTextGO.GetComponent<TMP_Text>();
        }
    }

    public void InitUI()
    {
        if(isLocalPlayer) UpdateUI();
    }

    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    [Server]
    public void TakeDamage(int i)
    {
        // Check if the player has the ÷2 damage perk
        // var selectedPerk = GameManager.Instance.GetSelectedPerk();

        /*if (selectedPerk == PerkSystem.PerkType.HalfDamage)
        {
            damage /= 2; // Reduce damage by half
        }
        
        if (selectedPerk == PerkSystem.PerkType.DoubleDamage)
        {
            damage *= 2; // Multiply damage by 2
        }*/

        currentHealth -= i;
        Debug.Log($"Player took {i} damage. Current health: {currentHealth}");
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    [Server]
    private void Die()
    {
        Debug.Log("Player died!");
        RpcHandleDeath();
        
        // Add death handling logic here (e.g., respawn, end game)
    }
    
    [ClientRpc]
    private void RpcHandleDeath()
    {
        if (!isLocalPlayer) return;

        GetComponent<PlayerScript>().disabled = true;
        GetComponentInChildren<MouseLook>().disabled = true;

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }

        var respawn = GameObject.FindWithTag("RespawnButton");
        // Show respawn UI
        if (respawn)
        {
            if (respawn.TryGetComponent<Canvas>(out var canvas))
            {
                canvas.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Canvas is active");
            }
        }
    }
    
    [Server]
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    private void OnHealthChanged(int oldHealth, int newHealth)
    {
        Debug.Log($"Health updated: {newHealth}");
        if(isLocalPlayer) UpdateUI();
    }
}