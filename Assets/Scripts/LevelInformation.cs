using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformation : MonoBehaviour
{
    [Header("Boundary Parameters")]
    [SerializeField, Tooltip("Horizontal range of the Boundary")]
    float xRange;
    [SerializeField, Tooltip("Vertical range of the Boundary")]
    float zRange;

    public Boundary Boundary { get; private set; } // ENCAPSULATION

    void Awake()
    {
        Boundary = new Boundary(xRange, zRange);
    }
}
