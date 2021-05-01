using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanEnemy : BaseEntity
{
    public Material nyaMaterial;
    public Material defaultMaterial;
    public Renderer Child;


    [SerializeField] private float Knock = 0;

    bool nya = false;
    float myan = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        Debug.Log("Nya");
        nya = true;

        Transform parent = collision.gameObject.GetComponent<Sword>().parentTransform;

        Vector3 direction = transform.position - parent.position;
        direction.y = 0;
        rb.AddForce(direction.normalized * Knock, ForceMode.Impulse);
    }
}
