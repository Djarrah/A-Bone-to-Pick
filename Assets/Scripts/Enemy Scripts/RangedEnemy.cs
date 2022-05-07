using System.Collections;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] GameObject projectile;
    
    protected override void Movement()
    {
        base.Movement();
        KeepDistance();
    }

    void KeepDistance()
    {
        if (distance > attackRange * 3 / 4)
        {
            Approach();
        }
        else if (distance < attackRange / 2)
        {
            transform.position -= MoveVector();
        }
    }

    protected override void Attack()
    {
        base.Attack();
        
        SpawnProjectile();
    }

    void SpawnProjectile()
    {
        GameObject shootProjectile = Instantiate(
            projectile,
            transform.position + transform.forward,
            transform.rotation
            );
    }
}