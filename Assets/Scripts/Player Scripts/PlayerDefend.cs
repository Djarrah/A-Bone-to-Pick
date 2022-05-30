using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefend : MonoBehaviour
{
    PlayerAttack playerAttack;

    GameObject shieldArea;

    [SerializeField, Tooltip("Maximum Shield Health")]
    int shieldHealthMax = 3;
    int shieldHealth = 3;
    [SerializeField, Tooltip("Health regenerated every tick")]
    int regenRate = 1;

    [SerializeField, Tooltip("Seconds to form a tick")]
    float regenTime = 1.0f;
    [SerializeField, Tooltip("Seconds to repair a broken shield")]
    float regenDelay = 3.0f;
    float timer = 0;

    public bool Defending { get; private set; } = false; // ENCAPSULATION
    bool shieldBroken = false;

    Animator anim;
    [SerializeField] ShieldBar shieldBar;

    [SerializeField] AudioSource effectSource;
    [SerializeField] AudioClip shieldClip;


    void Start()
    {
        // initializing variables
        shieldArea = transform.GetChild(0).gameObject;
        playerAttack = GetComponent<PlayerAttack>();
        anim = GetComponentInChildren<Animator>();

        // replenishes shield
        shieldHealth = shieldHealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.GameActive) { return; }
        
        if (Input.GetKey(KeyCode.LeftShift) && CanDefend())
        {
            Defend();

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopDefend();
        }

        if (!Defending) { RegenShield(); }
    }

    // Damage shield health, break shield if it reaches 0
    public void DamageShield(int amount)
    {
        shieldHealth -= amount;
        shieldBar.SetHealth(shieldHealth);
        effectSource.PlayOneShot(shieldClip);

        Debug.Log($"Shield Health: {shieldHealth}");

        if (shieldHealth <= 0)
        {
            shieldHealth = 0;
            shieldBroken = true;
            StopDefend();

            Debug.Log("Shield Broken!");
        }
    }

    // Checks if player can raise shield: not attacking, shield not broken and not already defending
    bool CanDefend()
    {
        return !Defending && !playerAttack.Attacking && !shieldBroken;
    }

    // activates shield area
    void Defend()
    {
        Defending = true;
        shieldArea.SetActive(Defending);
        anim.SetBool("defending", Defending);
    }

    // deactivates shield area
    void StopDefend()
    {
        Defending = false;
        shieldArea.SetActive(Defending);
        anim.SetBool("defending", Defending);
    }

    // replenishes shield health every tick, or repairs the shield if broken
    void RegenShield()
    {
        if (shieldHealth == shieldHealthMax) { return; } // prevent health overflow
        
        timer += Time.deltaTime;
        if (!shieldBroken && timer >= regenTime)
        {
            timer = 0.0f;
            shieldHealth += regenRate;

            Debug.Log($"Shield Health: {shieldHealth}");
        }
        else if (shieldBroken && timer >= regenDelay)
        {
            timer = 0.0f;
            shieldHealth = 1;
            shieldBroken = false;

            Debug.Log("Shields up!");
        }

        shieldBar.SetHealth(shieldHealth);
    }
}
