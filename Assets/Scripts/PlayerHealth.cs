using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    public float maxHealth = 100f;
    
    [SyncVar(hook = nameof(OnHealthChanged))]
    private float currentHealth;

    private void Start()
    {
        if (isServer)
        {
            currentHealth = maxHealth;
        }
    }

    [Server]
    public void TakeDamage(float damage)
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

    private void OnHealthChanged(float oldHealth, float newHealth)
    {
        Debug.Log($"Health updated: {newHealth}");
        // Update UI or visuals on clients here
    }
}

