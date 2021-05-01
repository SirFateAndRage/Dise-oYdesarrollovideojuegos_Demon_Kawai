using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dummy : MonoBehaviour
{
    public Material nyaMaterial;
    public Material defaultMaterial;

    public Renderer Child;
    public Rigidbody rb;

    private float Knock = 1;

    bool nya = false;
    float myan = 0;

    void Update()
    {
      
        if (nya)
        {
            Child.material = nyaMaterial;
            myan += Time.deltaTime;
            if (myan > 0.25f)
                nya = false;
        }
        else
        {
            myan = 0;
            Child.material = defaultMaterial;
        }
    }




    private void OnTriggerEnter(Collider collision)
    {

        nya = true;
        
        Transform parent = collision.gameObject.GetComponent<Sword>().parentTransform;
        
        Vector3 direction = transform.position - parent.position;
        direction.y = 0;
        rb.AddForce(direction.normalized * Knock, ForceMode.Impulse);
    }


}
