using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores various information about the level, such as the bounds
public class LevelInformation : MonoBehaviour
{
    public static LevelInformation Instance;
    
    [Header("Boundary Parameters")]
    [SerializeField, Tooltip("Horizontal range of the Boundary")]
    float xRange;
    [SerializeField, Tooltip("Vertical range of the Boundary")]
    float zRange;

    public Boundary Bounds { get; private set; } // ENCAPSULATION

    void Awake()
    {
        Instance = this;

        Bounds = new Boundary(xRange, zRange);
    }
}
