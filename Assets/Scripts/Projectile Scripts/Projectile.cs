using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used by projectiles going forward
public class Projectile : MonoBehaviour
{
    [SerializeField] bool friendlyFire = false;

    [SerializeField] int damage = 1;

    [SerializeField] float projectileSpeed = 1;

    [SerializeField] GameObject explosion;
    
    private void Update()
    {
        Movement();
        //DestroyOutOfBounds();
    }

    // Damages those who come in contact with the projectile
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;

        switch (target.tag)
        {
            case "Shield": // the shield, if raised...
                target.transform.parent.gameObject.GetComponent<PlayerDefend>().DamageShield(damage);
                break;

            case "Player": // the player...
                target.GetComponent<Health>().Damage(damage);
                break;

            case "Enemy": // even enemies if friendly fire is enabled for this projectile!
                if (friendlyFire)
                {
                    target.GetComponent<Health>().Damage(damage);
                }
                break;
        }

        DestroyProjectile(); // pops the projectile on every hit, even a wall
    }

    // The way the projectile pops
    protected void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    // Moves forward, overridden by the homing variant
    protected virtual void Movement()
    {
        transform.position += Time.deltaTime * projectileSpeed * transform.forward;
    }

    //// Destroys the projectile if it goes out of bounds
    //private void DestroyOutOfBounds()
    //{
    //    if (!LevelInformation.Instance.Bounds.IsInBounds(transform.position))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
