using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    protected CapsuleCollider capCollider;
    protected Rigidbody rb;

    

    
    void Awake()
    {
        capCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }
}
