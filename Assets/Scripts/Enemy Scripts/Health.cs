using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Tooltip("Maximum Health of the Creature")]
    int maxHealth = 1;
    int health;

    bool invulnerable = false;

    [SerializeField] float invulnerabilityTime = 1.0f;
    float invulnerabilityTimer = 0.0f;
    [SerializeField] float blinkTime = 0.25f;
    float blinkTimer = 0.0f;

    [SerializeField] Renderer[] renderers;

    protected Animator animator;

    private void Start()
    {
        health = maxHealth; // Sets health to full
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (invulnerable) { WaitInvulnerability(); }
    }

    void WaitInvulnerability()
    {
        /* why does it alternate between disabled renders
        and enabled renders at the end of each blink? */
        blinkTimer += Time.deltaTime;
        if (blinkTimer >= blinkTime)
        {
            blinkTimer = 0.0f;
            BlinkRenderers();
        }

        invulnerabilityTimer += Time.deltaTime;
        if (invulnerabilityTimer >= invulnerabilityTime)
        {
            invulnerabilityTimer = 0.0f;
            invulnerable = false;

            // It's a stupid fix but it just works
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
        }
    }

    void BlinkRenderers()
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = !r.enabled;
        }
    }

    // check to see if entity is at full health
    public bool IsFullyHealed()
    {
        return health == maxHealth;
    }
    
    // replenishes the given amount of health
    public void Heal(int amount = 1)
    {
        health += amount;
        Debug.Log($"{name}'s health: {health}");

        if (health > maxHealth) // prevents health overflow
        {
            health = maxHealth;
        }

        // heal fx around target?
    }

    // depletes the given amount of health
    public void Damage(int amount = 1)
    {
        if (invulnerable) { return; }

        invulnerable = true;
        BlinkRenderers();

        health -= amount;
        Debug.Log($"{name}'s health: {health}");

        if (health <= 0) // triggers death if HP reaches 0 or lower
        {
            Death();
        }
    }

    // to be completed
    protected virtual void Death()
    {
        Debug.Log($"{gameObject.name} has died!");
        if (animator != null) // DEBUG
        {
            animator.SetBool("dead", true);
        }

        StartCoroutine(PostDeath());
    }

    IEnumerator PostDeath()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Enemy>().Dead = true;

        yield return new WaitForSeconds(2);

        // smoke puff
        gameObject.SetActive(false);
    }
}
