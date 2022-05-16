using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Tooltip("Duration of the attack")]
    float attackDuration = 0.25f;
    float timer = 0.0f;
    
    /* Legacy section
    [SerializeField, Tooltip("Range of the attack")]
    float attackRange = 1.0f;

    [SerializeField, Tooltip("Damage inflicted with each hit")]
    int damage = 1;
    */

    [SerializeField] GameObject sword;

    public bool Attacking { get; private set; } = false; // ENCAPSULATION

    Animator animator;
    PlayerDefend playerDefend;

    // Start is called before the first frame update
    void Start()
    {
        // initializes variables
        playerDefend = GetComponent<PlayerDefend>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.GameActive) { return; }
        
        if (Input.GetKeyDown(KeyCode.Space) && CanAttack())
        {
            // Attack(); Legacy
            SwordAttack();
        }

        if (Attacking)
        {
            AttackCooldown();
        }
    }

    // checks if player can attack
    bool CanAttack()
    {
        return (!playerDefend.Defending && !Attacking);
    }

    /* project a raycast to attack, legacy
    void Attack()
    {
        Attacking = true;
        animator.SetTrigger("attacking");

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, attackRange))
        {
            GameObject target = hit.transform.gameObject;

            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth == null) { return; } // prevents damaging invincible colliders

            targetHealth.Damage(damage);
        }
    }
    */

    // activates the sword's hitbox
    void SwordAttack()
    {
        Attacking = true;
        animator.SetTrigger("attacking");

        sword.SetActive(Attacking);
    }

    // makes player able to attack after a duration
    void AttackCooldown()
    {
        timer += Time.deltaTime;

        if (timer >= attackDuration)
        {
            timer = 0.0f;
            Attacking = false;

            sword.SetActive(Attacking);
        }
    }
}
