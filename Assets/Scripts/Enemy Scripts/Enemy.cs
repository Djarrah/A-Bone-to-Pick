using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] protected float attackRange;
    [SerializeField] float attackCooldown;
    float timer = 0.0f;

    protected bool onCooldown = false;

    [Header("Movement")]
    [SerializeField] protected float walkSpeed;
    protected float distance;
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

    bool PlayerInRange()
    {
        return (distance <= attackRange);
    }

    protected Vector3 MoveVector()
    {
        return Time.deltaTime * walkSpeed * transform.forward;
    }

    void GetDistance()
    {
        distance = Vector3.Distance(
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
    }

    void Constrain()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bounds.xMin, bounds.xMax),
            transform.position.y,
            Mathf.Clamp(transform.position.z, bounds.zMin, bounds.zMax)
            );
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

    protected virtual void Attack()
    {
        onCooldown = true;
    }

    protected virtual void AttackRoutine()
    {
        if (onCooldown) { RefreshAttack(); }
        else if (PlayerInRange()) { Attack(); }
    }
}