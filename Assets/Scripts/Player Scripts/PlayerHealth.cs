using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    // INHERITANCE

    GameManager gameManager;

    // POLYMORPHISM
    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    protected override void Death()
    {
        gameManager.GameOver = true;
        base.Death();
    }
}
