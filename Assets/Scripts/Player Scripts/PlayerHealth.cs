using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    // INHERITANCE

    // POLYMORPHISM
    // Triggers Game Over
    protected override void Death()
    {
        GameManager.Instance.GameOver();
        animator.SetBool("dead", true);
    }
}
