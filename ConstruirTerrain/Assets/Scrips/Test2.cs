using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    [SerializeField]
    private HealtBarr _healtbar;

    public float currentLife;
    public float maxLife = 100;
    public bool alive;
    [SerializeField]
     GameObject respawn;


    private void Start()
    {
        _healtbar.SetSize(1f);
        currentLife = maxLife;
        gameObject.transform.position = respawn.transform.position;
    }

    public void RecibeDamage(float dmg)
    {
        currentLife -= dmg;
        _healtbar.SetSize(currentLife / maxLife);

        if (currentLife <= 0)
        {
            currentLife = 0;
           
            //Respawn();

        }

        



        Debug.Log(currentLife);

    }


}
