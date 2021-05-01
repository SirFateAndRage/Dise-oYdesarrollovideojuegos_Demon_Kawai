using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestPlayer : BasePlayer
{
    protected float currentLife;
    private float maxLife = 100;
    public bool alive;


    private void Start()
    {
        currentLife = maxLife;
    }

    public void RecibeDamage(float dmg)
    {
        currentLife -= dmg;
        if (!alive)
        {
            return;
        }
        if (currentLife <= 0)
        {
            currentLife = 0;
            alive = !alive;
            gameObject.SetActive(false);
        }

        Debug.Log(currentLife);
        
    }

}

