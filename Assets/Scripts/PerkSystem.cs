using System.Collections;
using Mirror;
using UnityEngine;

public class PerkSystem : NetworkBehaviour
{
    public enum PerkType { DoubleDamage=0, HalfDamage=1, Teleportation=2, AimAssist=3 }

    [Header("Perk Settings")]
    [SyncVar]
    public PerkType selectedPerk;
    public float perkDuration = 30f; // Duration for x2 damage, ÷2 damage, and aim assist
    public float teleportRadius = 10f; // Radius for teleportation
    public float cooldownTime = 10f; // Cooldown for all perks

    private bool isPerkActive = false;
    private bool isCooldownActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Example input for activating the perk
        {
            ActivatePerk();
        }
    }

    void ActivatePerk()
    {
        if (isPerkActive || isCooldownActive) return;

        switch (selectedPerk)
        {
            case PerkType.DoubleDamage:
                StartCoroutine(HandleDamageModifier(2f));
                break;
            case PerkType.HalfDamage:
                StartCoroutine(HandleDamageModifier(0.5f));
                break;
            case PerkType.Teleportation:
                Teleport();
                break;
            case PerkType.AimAssist:
                StartCoroutine(HandleAimAssist());
                break;
        }
    }

    IEnumerator HandleDamageModifier(float modifier)
    {
        isPerkActive = true;
        // Apply damage modifier logic here (e.g., adjust weapon damage)
        Debug.Log($"Damage modifier activated: x{modifier}");
        yield return new WaitForSeconds(perkDuration);
        // Reset damage modifier logic
        Debug.Log("Damage modifier deactivated.");
        StartCooldown();
    }

    void Teleport()
    {
        isPerkActive = true;
        Vector3 teleportPosition = GetTeleportPosition();
        if (teleportPosition != Vector3.zero)
        {
            transform.position = teleportPosition;
            Debug.Log("Teleported to: " + teleportPosition);
        }
        else
        {
            Debug.Log("No valid teleport position within range.");
        }
        StartCooldown();
    }

    Vector3 GetTeleportPosition()
    {
        // Generate a random position within the teleportRadius
        Vector3 randomDirection = Random.insideUnitSphere * teleportRadius;
        randomDirection.y = 0; // Ensure the teleportation remains on the same height
        Vector3 targetPosition = transform.position + randomDirection;

        // Check if the target position is valid (e.g., not obstructed)
        if (Physics.Raycast(targetPosition + Vector3.up, Vector3.down, out RaycastHit hit, 2f))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    IEnumerator HandleAimAssist()
    {
        isPerkActive = true;
        // Implement aim assist logic here
        Debug.Log("Aim assist activated.");
        yield return new WaitForSeconds(perkDuration);
        Debug.Log("Aim assist deactivated.");
        StartCooldown();
    }

    void StartCooldown()
    {
        isPerkActive = false;
        isCooldownActive = true;
        StartCoroutine(CooldownRoutine());
    }

    IEnumerator CooldownRoutine()
    {
        Debug.Log("Perk on cooldown...");
        yield return new WaitForSeconds(cooldownTime);
        Debug.Log("Perk ready.");
        isCooldownActive = false;
    }
}
