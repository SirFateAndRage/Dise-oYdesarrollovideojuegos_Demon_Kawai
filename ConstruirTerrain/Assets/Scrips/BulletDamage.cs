using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    float damage = 10f;
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<MainPlayer>())
        {
            col.gameObject.GetComponent<MainPlayer>().TakeDamage(damage);

            this.enabled = false;
            this.gameObject.layer = 31;

        }

        if (col.gameObject.layer != 10)
        {
            this.enabled = false;
            this.gameObject.layer = 31;

        }
    }

}
