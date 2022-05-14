using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    // INHERITANCE

    // POLYMORPHISM

    protected override void Death()
    {
        GameManager.Instance.GameOver();
        base.Death();
    }
}
