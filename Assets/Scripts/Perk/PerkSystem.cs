using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSystem : MonoBehaviour
{
   public enum PerkType
   { DoubleDamage, HalfDamage, Teleportation, AimAssist }

   [Header("Perk Settings")] public PerkType selectedPerk;
   public float perkDuration = 30f; // Duration for x2 damage and /2 damage and aim assist

   public float teleportRadius = 10f;

   public float cooldownTime = 10f;

   private bool isPerkActive = false;
   private bool isCooldownActive = false;

   void Start()
   {
      selectedPerk = PerkType.Teleportation;
   }

   /*void Update()
   {
      if (Input.GetKeyDown(KeyCode.P))
      {
         ActivatePerk();
      }
   }*/

   /*void ActivatePerk()
   {
      if (!isPerkActive || isCooldownActive) return;

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
   }*/

   IEnumerator HandleDamageModifier(float modifier)
   {
      isPerkActive = true;
      Debug.Log($"Damage modifier activated: x{modifier}");
      yield return new WaitForSeconds(perkDuration);
      Debug.Log("Damage Modifier deactivated");
      //StartCooldown();
   }

   void Teleport()
   {
      isPerkActive = true;
      Vector3 teleportPosition = GetTeleportPosition();
      if (teleportPosition != Vector3.zero)
      {
         transform.position = teleportPosition;
         Debug.Log("Teleported to:" + teleportPosition);
      }
      else
      {
         Debug.Log("No valid teleport position within range");
      }

      //StartCooldown();
   }

   Vector3 GetTeleportPosition()
   {
      Vector3 randomDirection = Random.insideUnitSphere * teleportRadius;
      randomDirection.y = 0;
      Vector3 targetPosition = transform.position + randomDirection;

      if (Physics.Raycast(targetPosition + Vector3.up, Vector3.down, out RaycastHit hit, 2f))
      {
         return hit.point;
      }

      return Vector3.zero;
   }
}
