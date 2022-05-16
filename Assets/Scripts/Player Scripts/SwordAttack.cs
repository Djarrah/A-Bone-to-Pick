using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    int attackValue = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health targetHealth = other.GetComponent<Health>();
            targetHealth.Damage(attackValue);
        }
    }
}
