using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float lifetime = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", lifetime);
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
