using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Tooltip("Maximum Health of the Creature")]
    int maxHealth = 1;
    int health = 1;

    private void Start()
    {
        health = maxHealth; // Sets health to full
    }

    // check to see if entity is at full health
    public bool IsFullyHealed()
    {
        return health == maxHealth;
    }
    
    // replenishes the given amount of health
    public void Heal(int amount = 1)
    {
        health += amount;
        Debug.Log($"{name}'s health: {health}");

        if (health > maxHealth) // prevents health overflow
        {
            health = maxHealth;
        }

        // heal fx around target?
    }

    // depletes the given amount of health
    public void Damage(int amount = 1)
    {
        health -= amount;
        Debug.Log($"{name}'s health: {health}");

        if (health <= 0) // triggers death if HP reaches 0 or lower
        {
            Death();
        }

        // maybe trigger hurt animation?
    }

    // to be completed
    void Death()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject);
        // death animation
    }
}
