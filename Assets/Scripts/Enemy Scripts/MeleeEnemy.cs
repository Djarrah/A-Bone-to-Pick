using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for enemies who attack in melee range with weapons
public class MeleeEnemy : Enemy // INHERITANCE
{
    [SerializeField, Tooltip("Health removed with each hit")]
    int damage;

    [SerializeField, Tooltip("Can this enemy hit their allies?")]
    bool friendlyFire = false;

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
                    target.transform.parent.gameObject.GetComponent<PlayerDefend>().DamageShield(damage);
                    break;

                case "Player":
                    target.GetComponent<Health>().Damage(damage);
                    break;

                case "Enemy":
                    if (friendlyFire)
                    {
                        target.GetComponent<Health>().Damage(damage);
                    }
                    break;
            }
        }
    }
}
