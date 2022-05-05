using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAttack : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("Attack", 2.0f, 3.0f);
    }

    void Attack()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, 2.0f))
        {
            GameObject target = hit.collider.gameObject;

            switch (target.tag)
            {
                case "Shield":
                    GameObject parent = target.transform.parent.gameObject;
                    PlayerDefend targetShield = parent.GetComponent<PlayerDefend>();
                    targetShield.DamageShield(1);

                    Debug.DrawLine(origin, hit.point, Color.yellow, 0.25f);
                    break;

                case "Player":
                    Health targetHealth = target.GetComponent<Health>();
                    targetHealth.Damage(1);

                    Debug.DrawLine(origin, hit.point, Color.red, 0.25f);
                    break;
            }
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * 2.0f, Color.green, 0.25f);
        }
    }
}
