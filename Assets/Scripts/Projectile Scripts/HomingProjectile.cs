using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    [SerializeField] float projectileLife;

    GameObject homingTarget;
    
    protected override void Start()
    {
        base.Start();
        homingTarget = GameObject.Find("Player");

        Invoke("DestroyProjectile", projectileLife);
    }

    protected override  void Update()
    {
        RotateToPlayer();
        base.Update();
    }

    void RotateToPlayer()
    {
        transform.LookAt(homingTarget.transform.position);
    }
}
