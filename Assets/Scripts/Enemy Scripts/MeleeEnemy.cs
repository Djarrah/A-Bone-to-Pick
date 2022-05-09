using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for enemies who attack in melee range with weapons
public class MeleeEnemy : Enemy // INHERITANCE
{
    [SerializeField, Tooltip("Health removed with each hit")]
    int damage;

    // POLYMORPHISM
    // Overrides the base method approaching the player until in melee range
    protected override void Movement()
    {
        base.Movement();

        if (!PlayerInRange())
        {
            Approach();
        }
    }

    // Overrides the base method using a Raycast to attack in melee
    protected override void Attack()
    {
        base.Attack();
        
        if (
            Physics.Raycast(
                transform.position,
                transform.forward,
                out RaycastHit hit,
                attackRange
                )
            )

        {
            GameObject target = hit.collider.gameObject;

            switch (target.tag)
            {
                case "Shield":
                    PlayerDefend targetShield = target.transform.parent.gameObject.GetComponent<PlayerDefend>();
                    targetShield.DamageShield(damage);
                    break;

                case "Player":
                    Health targetHealth = target.GetComponent<Health>();
                    targetHealth.Damage(damage);
                    break;
            }
        }
    }
}
