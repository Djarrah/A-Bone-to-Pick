using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    // INHERITANCE

    // POLYMORPHISM
    protected override void Movement()
    {
        base.Movement();
        transform.position += Time.deltaTime * walkSpeed * transform.forward;
    }

    protected override void Attack()
    {
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

            onCooldown = true;
        }
    }
}
