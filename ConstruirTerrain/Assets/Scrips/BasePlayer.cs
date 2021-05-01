using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseCombatEntity
{



    [SerializeField] protected float JumpForceUp = 250f;
    [SerializeField] protected float JumpForceForward = 50f;

    [SerializeField] protected Sword sword;

    [SerializeField] protected CapsuleCollider basePlayerCollider;
    
    [SerializeField] protected CapsuleCollider envCollider;
    [SerializeField] protected float speed = 6f;
}
