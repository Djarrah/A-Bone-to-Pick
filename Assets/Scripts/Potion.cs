using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField, Tooltip("Amount of health to heal when picked up")]
    int heal = 1;
    
    // health pickup
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // activable only by player
        {  
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth.IsFullyHealed()) { return; } // doesn't activate if player is full

            playerHealth.Heal(heal);
            Debug.Log($"Healed player for {heal} HP");

            Destroy(gameObject);
        }
    }
}
