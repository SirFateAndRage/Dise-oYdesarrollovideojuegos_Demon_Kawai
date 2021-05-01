using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainPlayer : BasePlayer
{
    public Transform cam;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float smoothBlend = 0.1f;

    public CameraController camControl;
    private Vector3 direction;


    public bool IsAlive;

    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        withSword = false;
        rb = GetComponent<Rigidbody>();
        envCollider= GetComponent<CapsuleCollider>();
        //anim.SetLayerWeight(0, 0);
        //anim.SetLayerWeight(2, 0);
        //anim.SetLayerWeight(1, 1);

    }




    int animLayer = 2;
    float nya = 0;
    public bool withSword = true;
    Vector3 direction2;
    void FixedUpdate()
    {

        if (health <= 0) IsAlive = false;
        else IsAlive = true;
        AnimatorStates();
        SwitchSword();

        //ParaOptimizar();


        if (!IsSwitching)
        {

            //if (!anim.GetBool("Run")&& !anim.GetBool("endTakeSword") && withSword) 
            //if (!anim.GetBool("Run"))
            //Attack();

            //if (!anim.GetBool("endCombo") && !anim.GetBool("endTakeSword")) 
            //if (!anim.GetBool("endCombo"))

            if (!IsAttack)
                Move();
            else
                AutoMove();

        }
        if (anim.GetBool("Run"))
        {
            if (anim.GetFloat("BlendRun") < 1)
                anim.SetFloat("BlendRun", (anim.GetFloat("BlendRun") + Time.deltaTime));
            else
                anim.SetFloat("BlendRun", 1f);
        }
        else
        {
            if (anim.GetFloat("BlendRun") > 0.5f)
                anim.SetFloat("BlendRun", (anim.GetFloat("BlendRun") - Time.deltaTime));
            else
                anim.SetFloat("BlendRun", 0.5f);
        }
        speed = 4.6f * anim.GetFloat("BlendRun");
    }


    public Material nyaMaterial;
    public Material defaultMaterial;
    public Renderer Child;

    
    float myan = 0;

    private void Update()
    {
        if (nyan)
        {
            Child.material = nyaMaterial;
            myan += Time.deltaTime;
            if (myan > 0.25f)
                nyan = false;


        }
        else
        {
            myan = 0;
            Child.material = defaultMaterial;
        }

            Jump();
        Sprint();
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPommelAttack || IsKickAttack || IsSlashC || IsComboB || IsComboC || IsShortSpin || IsLargeSpin || IsJumpAttack || IsSwitching) return;
            anim.SetBool("Mouse 0", true);
            anim.SetBool("BackToIdle", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (IsPommelAttack || IsComboB || IsJogging || IsIdle)
            {
                anim.SetBool("Mouse 1", true);
                anim.SetBool("BackToIdle", false);
            }
        }
            
        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");

        if (IsAttack)
        {
            anim.SetFloat("BlendAttack", anim.GetFloat("BlendAttack") + Time.deltaTime);

        }
        else
            anim.SetFloat("BlendAttack", 0);
    }

    public void BackToIdle()
    {
        anim.SetBool("BackToIdle", true);
    }
    public void ActiveTrigger(int nya) => sword.ActiveTrigger(nya);
    public void IsAttacking(int nya)
    {
        if (nya == 1)
            IsAttack = true;
        else
            IsAttack = false;
    }
    
    private void Move()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            anim.SetFloat("Direction", direction.magnitude, smoothBlend, Time.deltaTime);
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camControl.transform.rotation.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 MoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.MovePosition(transform.position + MoveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            JumpForceUp = 250f;
            JumpForceForward = 0f;
        }

    }

    private void AutoMove()
    {
        direction = new Vector3(0, 0, (anim.GetFloat("Movement"))).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 MoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(transform.position + MoveDir.normalized * speed * Time.deltaTime * anim.GetFloat("Movement"));
        }

    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("Run", true);
            //speed *= 2;
            JumpForceUp = 300f;
            JumpForceForward = 100f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Run", false);
            //speed = 2.3f;
            JumpForceUp = 300f;
            JumpForceForward = 50f;
        }
    }

    private void Jump()
    {

        if (!IsJumping && IsGround)
        {
            anim.SetFloat("BlendJump", 0);                
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Jump", true);
                rb.AddForce(transform.up * JumpForceUp, ForceMode.Impulse);
                rb.AddForce(transform.forward * JumpForceForward, ForceMode.Impulse);
                //rb.velocity = new Vector3(0, JumpForce, 0); 
                camControl.NoJump(transform.position.y, true);
                return;
            }
            camControl.NoJump(transform.position.y, false);
        }
        else if (IsJumping)
        {
            float nya = anim.GetFloat("ColliderHeight");
            envCollider.height = nya;
            envCollider.center = new Vector3(0, ((1.8f - nya) / 2), 0);
            if(!IsJogging && !IsRunning)
                anim.SetFloat("BlendJump", (anim.GetFloat("BlendJump") + Time.deltaTime * 1.5f));
        } 
    }

    private void SwitchSword()
    {
        if (Input.GetKeyDown(KeyCode.R) && !IsSwitching)
        {
            
            withSword = !withSword;
            
            camControl.SwitchCamera(withSword);
            if (!withSword)
            {
                anim.SetInteger("CurrentLayer", 1);
                anim.SetLayerWeight(1, 1);
                anim.SetLayerWeight(2, 0);
                //animLayer = 1;
            }
            else
            {
                anim.SetInteger("CurrentLayer", 2);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(2, 1);
                
                //animLayer = 2;
            }
            //animLayer = 1;
            //anim.SetBool("TakeSword", true);
            //anim.SetBool("SwitchSword", true);
            //anim.SetBool("endTakeSword", true);
            
        }
        if (IsSwitching)
        {

            anim.SetFloat("BlendSword", (anim.GetFloat("BlendSword") + Time.deltaTime * 0.528f));
            //anim.SetFloat("BlendSword", 0.66f);
        }
        else// if(anim.GetFloat("BlendSword") > 1)
            anim.SetFloat("BlendSword", 0);
    }

    
    public bool IsGround;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            IsGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            IsGround = false;
    }


    public bool IsIdle;
    public bool IsJogging;
    public bool IsRunning;
    public bool IsJumping;
    public bool IsSwitching;
    public bool IsAttack;
    
    public bool IsSlashA;
    public bool IsSlashB;
    public bool IsSlashC;
    public bool IsComboA;
    public bool IsComboB;
    public bool IsComboC;
    public bool IsShortSpin;
    public bool IsLargeSpin;
    public bool IsJumpAttack;
    public bool IsPommelAttack;
    public bool IsKickAttack;

    private void AnimatorStates()
    {
        animLayer = anim.GetInteger("CurrentLayer");
        IsIdle = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsIdle");
        IsJogging = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsJogging");
        IsRunning = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsRunning");
        IsJumping = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsJumping");
        IsSwitching = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsSwitching");

        IsSlashA = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsSlashA");
        IsSlashB = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsSlashB");
        IsSlashC = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsSlashC");
        IsComboA = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsComboA");
        IsComboB = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsComboB");
        IsComboC = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsComboC");
        IsShortSpin = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsShortSpin");
        IsLargeSpin = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsLargeSpin");
        IsJumpAttack = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsJumpAttack");
        IsPommelAttack = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsPommelAttack");
        IsKickAttack = anim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsKickAttack");
    }

   


    void ParaOptimizar()
    {
        if (anim.GetBool("endCombo"))
        {
            nya += Time.deltaTime * 5f;
            anim.SetFloat("BlendAttack", nya);

            if (basePlayerCollider.radius < 0.5f)
                basePlayerCollider.radius += Time.deltaTime;
        }
        else
        {
            nya = 0;
            anim.SetFloat("BlendAttack", nya);
            basePlayerCollider.radius = 0.3f;
        }

        if (anim.GetBool("endBlendAttack"))
        {

            direction2 = new Vector3(0, 0, 2).normalized;
            if (direction2.magnitude >= 0.1f)
            {

                float targetAngle2 = Mathf.Atan2(direction2.x, direction2.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle2 = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle2, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle2, 0f);

                Vector3 MoveDir2 = Quaternion.Euler(0f, targetAngle2, 0f) * Vector3.forward;
                rb.MovePosition(transform.position + MoveDir2.normalized * speed * Time.deltaTime);
            }
        }

    }
    
}

