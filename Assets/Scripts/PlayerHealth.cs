using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class PlayerHealth : NetworkBehaviour
{
    public int maxHealth = 100;
    
    [SyncVar(hook = nameof(OnHealthChanged))]
    private int currentHealth;

    private TMP_Text healthText;

    private void Start()
    {
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

    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}/{maxHealth}";
        }
    }

    [Server]
    public void TakeDamage(int damage)
    {
        // Check if the player has the ÷2 damage perk
        var selectedPerk = GameManager.Instance.GetSelectedPerk();

        if (selectedPerk == PerkSystem.PerkType.HalfDamage)
        {
            damage /= 2; // Reduce damage by half
        }
        
        if (selectedPerk == PerkSystem.PerkType.DoubleDamage)
        {
            damage *= 2; // Multiply damage by 2
        }

        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [Server]
    private void Die()
    {
        Debug.Log("Player died!");
        // Add death handling logic here (e.g., respawn, end game)
    }
    
    [ClientRpc]
    private void RpcHandleDeath()
    {
        // Handle death visuals/sounds on the client
        Debug.Log("Player death handled on client.");
    }

    private void OnHealthChanged(int oldHealth, int newHealth)
    {
        Debug.Log($"Health updated: {newHealth}");
        UpdateUI();
    }
}

