using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    // INHERITANCE

    HealthBar healthBar;

    // POLYMORPHISM
    // Adds healthbar settings to the initialization
    protected override void Start()
    {
        base.Start();

        healthBar = HealthBar.Instance;
        healthBar.SetMaxHealth(health);
    }

    public override void Heal(int amount = 1)
    {
        base.Heal(amount);

        healthBar.SetHealth(health);
    }

    public override void Damage(int amount = 1)
    {
        base.Damage(amount);

        healthBar.SetHealth(health);
    }

    // Triggers Game Over
    protected override void Death()
    {
        GameManager.Instance.GameOver();
        animator.SetBool("dead", true);
    }
}
