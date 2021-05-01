using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMenu : MonoBehaviour
{
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        tf.rotation *= Quaternion.Euler(0f, 0.63f, 0f);
    }
}
