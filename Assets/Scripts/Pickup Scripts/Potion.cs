using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// restores player health on pickup
public class Potion : MonoBehaviour
{
    [Header("Floating")]
    [SerializeField] float frequency = 1;
    [SerializeField] float amplitude = 0.3f;
    Vector3 posOffset;

    [Header("Rotating")]
    [SerializeField] float rotationSpeed = 1;

    [Header("Healing")]
    [SerializeField, Tooltip("Amount of health to heal when picked up")]
    int heal = 1;

    private void Start()
    {
        posOffset = transform.position;
    }

    private void Update()
    {
        Hover();
        Rotate();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // activable only by player
        {  
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth.IsFullyHealed()) { return; } // doesn't activate if player is full

            playerHealth.Heal(heal);
            Debug.Log($"Healed player for {heal} HP");

            Destroy(gameObject);
        }
    }

    void Hover()
    {
        Vector3 tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed); 
    }
}
