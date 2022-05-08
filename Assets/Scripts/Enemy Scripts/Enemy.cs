using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all enemy types
public abstract class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float walkSpeed;
    protected float distanceToTarget;

    [Header("Combat")]
    [SerializeField] protected float attackRange;

    [SerializeField, Tooltip("Time before the attack is executed")]
    private float attackWindup;
    private float windupTimer = 0.0f;
    protected bool inWindup = false;
    protected bool attackReady = false;

    [SerializeField, Tooltip("Time between consecutive attacks")]
    private float attackCooldown;
    private float cooldownTimer = 0.0f;
    protected bool inCooldown = false;

    private Boundary bounds; // this level's bounds

    private GameObject player;

    // Called before the first frame, when script is initialized
    private void Start()
    {
        // initializing variables
        player = GameObject.Find("Player");

        // there has to be a simpler way to do this
        bounds = GameObject.Find("Level Information").GetComponent<LevelInformation>().Boundary;
    }

    // Called at each frame
    private void Update()
    {
        // if gameover return

        GetDistance();

        MovementRoutine();

        AttackRoutine();
    }

    // Simply sets the distance between Enemy and Player
    private void GetDistance()
    {
        distanceToTarget = Vector3.Distance(
            player.transform.position, transform.position
            );
    }

    // If player is not winding up an attack, moves according to its pattern and gets constrained by bounds
    private void MovementRoutine()
    {
        if (inWindup) { WindupAttack(); }
        else
        {
            Movement();
            Constrain();
        }
    }

    // Charges attack during the animation
    private void WindupAttack()
    {
        windupTimer += Time.deltaTime;

        if (windupTimer >= attackWindup)
        {
            windupTimer = 0.0f;
            inWindup = false;
            attackReady = true;
        }
    }

    // At base level, looks at the player. It is meant to be overridden by child classes
    protected virtual void Movement()
    {
        transform.LookAt(player.transform.position);
    }

    // Gets closer in the MoveVector direction
    protected void Approach()
    {
        transform.position += MoveVector();
        // forward walk
    }

    // Returns a Vector going forward according to speed
    protected Vector3 MoveVector()
    {
        return Time.deltaTime * walkSpeed * transform.forward;
    }

    // Constrains the enemy position in the Bounds' borders
    private void Constrain()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bounds.xMin, bounds.xMax),
            transform.position.y,
            Mathf.Clamp(transform.position.z, bounds.zMin, bounds.zMax)
            );
    }

    // Handles attack behaviour, overridden by Healer Enemies
    protected virtual void AttackRoutine()
    {
        if (attackReady) { Attack(); }
        else if (inCooldown) { RefreshAttack(); }
        else if (PlayerInRange() && !inWindup) // if player is in range and not already winding up...
        {
            inWindup = true; // starts winding up the attack
        }
    }

    // Handles the attack itself, overridden by child classes according to needs
    protected virtual void Attack()
    {
        transform.LookAt(player.transform);
        inCooldown = true;
        attackReady = false;
        // attack ani
        // pause move and rotation?
    }

    // Recharges the cooldown after an attack
    protected void RefreshAttack()
    {
        cooldownTimer += Time.deltaTime;
        Debug.Log("Refreshing attack!");

        if (cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0.0f;
            inCooldown = false;

            Debug.Log("I can attack again!");
        }
    }

    // Checks if player is within attack range
    protected bool PlayerInRange()
    {
        return (distanceToTarget <= attackRange);
    }
}