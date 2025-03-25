using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

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

    private void Die()
    {
        Debug.Log("Player died!");
        // Add death handling logic here (e.g., respawn, end game)
    }
}

