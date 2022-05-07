using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] protected float attackRange;
    [SerializeField] float attackCooldown;
    float timer = 0.0f;

    protected bool stopAttacking = false;

    [Header("Movement")]
    [SerializeField] protected float walkSpeed;
    protected float distanceToTarget;

    Boundary bounds;

    GameObject player;

    void Start()
    {
        // initializing variables
        player = GameObject.Find("Player");

        // there has to be a simpler way to do this
        bounds = GameObject.Find("Level Information").GetComponent<LevelInformation>().Boundary;
    }

    void Update()
    {
        // if gameover return

        GetDistance();  
        
        Movement();
        Constrain();

        AttackRoutine();
    }

    protected bool PlayerInRange()
    {
        return (distanceToTarget <= attackRange);
    }

    protected Vector3 MoveVector()
    {
        return Time.deltaTime * walkSpeed * transform.forward;
    }

    void GetDistance()
    {
        distanceToTarget = Vector3.Distance(
            player.transform.position, transform.position
            );
    }

    protected virtual void Movement()
    {
        transform.LookAt(player.transform.position);
    }

    protected void Approach()
    {
        transform.position += MoveVector();
        // forward walk
    }

    void Constrain()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bounds.xMin, bounds.xMax),
            transform.position.y,
            Mathf.Clamp(transform.position.z, bounds.zMin, bounds.zMax)
            );
    }

    protected void RefreshAttack()
    {
        timer += Time.deltaTime;

        if (timer >= attackCooldown)
        {
            timer = 0.0f;
            stopAttacking = false;
        }
    }

    protected virtual void Attack()
    {
        stopAttacking = true;
        // attack ani
        // pause move and rotation?
    }

    protected virtual void AttackRoutine()
    {
        if (stopAttacking) { RefreshAttack(); }
        else if (PlayerInRange()) { Attack(); }
    }
}