using System.Collections;
using UnityEngine;

// Class for enemies attacking at distance with projectiles
public class RangedEnemy : Enemy // INHERITANCE
{
    [SerializeField] GameObject projectile;
    
    // POLYMORPHISM
    // Overrides the base method keeping distant from the player
    protected override void Movement()
    {
        base.Movement();

        KeepDistance();
    }

    // Stays between 3/4 and 1/2 of the attack range
    private void KeepDistance()
    {
        if (distanceToTarget > attackRange * 3 / 4)
        {
            Approach();
            animator.SetFloat("direction", 1);
        }
        else if (distanceToTarget < attackRange / 2)
        {
            rb.position -= MoveVector();
            animator.SetFloat("direction", 0);
        }
    }

    // Overrides the base class attack by spawning a projectile
    protected override void Attack()
    {
        base.Attack();
        
        SpawnProjectile();
    }

    // Instantiates a projectiel in front of the enemy
    private void SpawnProjectile()
    {
        GameObject shootProjectile = Instantiate(
            projectile,
            transform.position + transform.forward,
            transform.rotation
            );
    }
}