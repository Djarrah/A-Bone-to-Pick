using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float attackDuration = 0.25f; // edit depending on model animation
    float timer = 0.0f;

    GameObject attackArea = default;

    bool attacking = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // retrieves the attack area child
        attackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        // prevents spamming
        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= attackDuration)
            {
                timer = 0.0f;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    // enables the attack area
    void Attack()
    {
        attacking = true;
        
        attackArea.SetActive(attacking);
    }
}
