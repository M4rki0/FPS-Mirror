using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    public int damage;

    public void TakeDamage()
    {
        currentHealth = maxHealth;
    }
}
