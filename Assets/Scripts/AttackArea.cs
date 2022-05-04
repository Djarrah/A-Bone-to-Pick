using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // If colliders are in the activated attack area, declare hit
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit!");
    }
}
