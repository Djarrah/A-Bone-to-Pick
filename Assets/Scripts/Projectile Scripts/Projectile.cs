using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] bool friendlyFire = false;

    [SerializeField] int damage = 1;

    [SerializeField] float projectileSpeed = 1;

    Boundary bounds;

    protected virtual void Start()
    {
        bounds = GameObject.Find("Level Information").GetComponent<LevelInformation>().Boundary;
    }

    protected virtual void Update()
    {
        Movement();
        DestroyOutOfBounds();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;

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

        DestroyProjectile();
    }

    protected void DestroyProjectile()
    {
        // effects
        Destroy(gameObject);
    }
    void Movement()
    {
        transform.position += Time.deltaTime * projectileSpeed * transform.forward;
    }

    void DestroyOutOfBounds()
    {
        if (!bounds.IsInBounds(transform.position))
        {
            Destroy(gameObject);
        }
    }
}
