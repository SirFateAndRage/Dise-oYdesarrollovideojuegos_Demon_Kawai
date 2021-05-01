using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCombatEntity : BaseEntity
{
    [SerializeField] protected Animator anim;

    public float maxHealth;
    public float health;

    protected float armor;

    protected bool nyan = false;
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        nyan = true;
    }
    protected virtual void Dead() { }
}
