using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For enemies able to heal their comrades
public class HealerEnemy : RangedEnemy // INHERITANCE
{
    [SerializeField, Tooltip("Health restored with each cast")]
    int healingValue;

    // will be filled at wave spawn, in order of priority.
    public List<GameObject> enemiesAlive;
    GameObject healingTarget = null;

    // POLYMORPHISIM
    // Overrides the base routine checking for damaged allies
    protected override void AttackRoutine()
    {
        if (healingTarget == null)
        {
            ChooseTarget();
        }
        // Resets target if ally has died during the windup
        else if (healingTarget.GetComponent<Enemy>().Dead) 
        {
            healingTarget = null;
        }

        // Switch to offensive mode if everyone is healed
        if (healingTarget == null)
        {
            base.AttackRoutine();
        }
        else
        { 
            HealRoutine(); 
        }
    }

    // Iterates through the active allies to check a damaged one
    private void ChooseTarget()
    {
        foreach (GameObject enemy in enemiesAlive)
        {
            if (!enemy.GetComponent<Health>().IsFullyHealed() && !enemy.GetComponent<Enemy>().Dead)
            {
                healingTarget = enemy;
                return;
            }
        }

        healingTarget = null;
    }

    // Charges up the heal before healing
    private void HealRoutine()
    {
        if (attackReady)
        {
            animator.SetTrigger("attack");
            healingTarget.GetComponent<Health>().Heal(healingValue);
            healingTarget = null;

            inCooldown = true;
            attackReady = false;
        }
        else if (inCooldown) { RefreshAttack(); }
        else if (!inWindup)
        { 
            inWindup = true;
            transform.LookAt(healingTarget.transform);
        }
    }
}
