using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Walking speed")]
    float walkSpeed;
    [SerializeField, Tooltip("Speed of the Y axis rotation when turning")]
    float rotationSpeed;

    Rigidbody rb;

    PlayerAttack playerAttack;

    Boundary bounds;

    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing variables
        rb = GetComponent<Rigidbody>();
        playerAttack = GetComponent<PlayerAttack>();
        bounds = LevelInformation.Instance.Bounds;
    }

    // FixedUpdate to handle physics
    void FixedUpdate()
    {
        if (!GameManager.Instance.GameActive)
        { 
            rb.velocity = Vector3.zero;
            return;
        }
        
        // ABSTRACTION
        
        GetInput();

        // maybe rework in a way similar to spiral knights?
        
        Rotate();
        Walk();

        Constrain();
    }

    // Stores the Inputs into variables for quicker use
    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // prevents moving if currently attacking
        if (playerAttack.Attacking)
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
    }

    // Rotates the player towards the direction they are headed to
    void Rotate()
    {
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        if (direction != Vector3.zero) // Prevents Vector3 zero console isues and rotation resets
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, rotation, rotationSpeed);
        }
    }
    
    // Moves the player following input system.
    void Walk()
    {
        rb.velocity = new Vector3(
            Mathf.Lerp(0, horizontalInput * walkSpeed, 0.8f),
            rb.velocity.y,
            Mathf.Lerp(0, verticalInput * walkSpeed, 0.8f)
            );
    }

    // Constrains the player in the given Boundary.
    void Constrain()
    {
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, bounds.xMin, bounds.xMax),
            rb.position.y,
            Mathf.Clamp(rb.position.z, bounds.zMin, bounds.zMax)
            );
    }
}
