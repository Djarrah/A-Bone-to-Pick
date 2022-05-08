using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child class of the projectile, this one follows the player
public class HomingProjectile : Projectile // INHERITANCE
{
    [SerializeField, Tooltip("Seconds after which the projectile will explode")]
    float projectileLife;

    GameObject homingTarget;
    
    // POLYMORPHISM
    protected override void Start()
    {
        base.Start();
        homingTarget = GameObject.Find("Player");

        Invoke("DestroyProjectile", projectileLife);
    }

    // Overrides base movement, facing the player before going forward
    protected override void Movement()
    {
        RotateToPlayer();
        base.Movement();
    }

    // rotates towards player
    private void RotateToPlayer()
    {
        transform.LookAt(homingTarget.transform.position);
    }
}
