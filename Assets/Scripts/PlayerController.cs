using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField, Tooltip("Horizontal range of the Boundary")] float xRange;
    [SerializeField, Tooltip("Vertical range of the Boundary")] float zRange;

    Rigidbody rb;
    Boundary bounds;

    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing variables
        rb = GetComponent<Rigidbody>();
        bounds = new Boundary(xRange, zRange);
    }

    // FixedUpdate to handle physics
    void FixedUpdate()
    {
        // ABSTRACTION
        
        GetInput();

        Rotate();
        Walk();

        Constrain();
    }

    // Stores the Inputs into variables for quicker use
    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
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
    
    // Moves the player following input system. Thanks to Troy Lamerton
    void Walk()
    {
        rb.velocity = new Vector3(
            Mathf.Lerp(0, horizontalInput * walkSpeed, 0.8f),
            rb.velocity.y,
            Mathf.Lerp(0, verticalInput * walkSpeed, 0.8f)
            );
    }

    // Constrains the player in the given Boundary. Thanks to Troy Lamerton
    void Constrain()
    {
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, bounds.xMin, bounds.xMax),
            rb.position.y,
            Mathf.Clamp(rb.position.z, bounds.zMin, bounds.zMax)
            );
    }
}
