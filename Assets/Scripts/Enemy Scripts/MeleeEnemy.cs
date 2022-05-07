using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    // INHERITANCE

    [SerializeField] int damage;

    // POLYMORPHISM
    protected override void Movement()
    {
        base.Movement();

        if (!PlayerInRange())
        {
            Approach();
        }
    }

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
