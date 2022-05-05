using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerDefend playerDefend;

    [SerializeField, Tooltip("Duration of the attack")]
    float attackDuration = 0.25f; // edit depending on model animation
    float timer = 0.0f;
    [SerializeField, Tooltip("Range of the attack")]
    float attackRange = 1.0f;

    [SerializeField, Tooltip("Damage inflicted with each hit")]
    int damage = 1;

    public bool Attacking { get; private set; } = false; // ENCAPSULATION
    
    // Start is called before the first frame update
    void Start()
    {
        // initializes variables
        playerDefend = GetComponent<PlayerDefend>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanAttack())
        {
            Attack();
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

    // project a raycast to attack
    void Attack()
    {
        //attack ani here
        Attacking = true;

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, attackRange))
        {
            GameObject target = hit.transform.gameObject;

            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth == null) { return; } // prevents damaging invincible colliders

            targetHealth.Damage(damage);

            Debug.DrawLine(origin, hit.point, Color.red, 0.25f);
        }
        else 
        { 
            Debug.DrawLine(origin, origin + direction * attackRange, Color.green, 0.25f);
        }
    }

    // makes player able to attack after a duration
    void AttackCooldown()
    {
        timer += Time.deltaTime;

        if (timer >= attackDuration)
        {
            timer = 0.0f;
            Attacking = false;
        }
    }
}
