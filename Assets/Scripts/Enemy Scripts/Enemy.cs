using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRange;
    [SerializeField] float attackCooldown;
    float timer = 0.0f;

    protected bool onCooldown = false;

    [Header("Movement")]
    [SerializeField] protected float walkSpeed;

    GameObject player;

    void Start()
    {
        // initializing variables
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Movement();

        if (onCooldown) { RefreshAttack(); }
        else if (PlayerInRange()) { Attack(); }
    }

    bool PlayerInRange()
    {
        float distance = Vector3.Distance(
            player.transform.position, transform.position
            );

        return (distance <= attackRange);
    }

    void RefreshAttack()
    {
        timer += Time.deltaTime;

        if (timer >= attackCooldown)
        {
            timer = 0.0f;
            onCooldown = false;
        }
    }

    protected virtual void Movement()
    {
        transform.LookAt(player.transform.position);
    }

    protected abstract void Attack();
}