using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerEnemy : RangedEnemy
{
    [SerializeField] int healingValue;

    // will be filled at wave spawn, in order of priority.
    // remove serializefield when waves are implemented
    [SerializeField] GameObject[] enemiesAlive;
    GameObject healingTarget = null;

    protected override void AttackRoutine()
    {
        ChooseTarget();
        
        if (healingTarget == null)
        {
            base.AttackRoutine();
        }
        else { HealRoutine(); }
    }

    void ChooseTarget()
    {
        foreach (GameObject enemy in enemiesAlive)
        {
            if (!enemy.GetComponent<Health>().IsFullyHealed())
            {
                healingTarget = enemy;
                return;
            }
        }

        healingTarget = null;
    }

    void HealRoutine()
    {
        if (stopAttacking) { RefreshAttack(); }
        else
        {
            // healani
            healingTarget.GetComponent<Health>().Heal(healingValue);

            stopAttacking = true;
        }
    }
}
