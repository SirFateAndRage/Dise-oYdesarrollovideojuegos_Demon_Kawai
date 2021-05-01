using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private int animLayer;
    private Animator playerAnim;
    private Renderer ren;
    private BoxCollider bx;

    public Transform parentTransform;
    public Transform handGrip;
    public Transform spineGrip;

    public Material SwordOn;
    public Material SwordOff;
    public GameObject Trail;

   
    private void Awake()
    {
        playerAnim = gameObject.GetComponentInParent<Animator>();
        ren = GetComponent<Renderer>();
        bx = GetComponent<BoxCollider>();
        animLayer = playerAnim.GetInteger("CurrentLayer");
        ChangeGrip();
    }
    void Start()
    {
    }

    void Update()
    {
        

        animLayer = playerAnim.GetInteger("CurrentLayer");
        if (playerAnim.GetCurrentAnimatorStateInfo(animLayer).IsTag("IsSwitching"))
                ChangeGrip();
    }

    public void ChangeGrip()
    {
        if ((animLayer == 1) && (playerAnim.GetFloat("BlendSword") > 0.33f))
            transform.parent = spineGrip;    
        else if ((animLayer == 2) && (playerAnim.GetFloat("BlendSword") > 0.66f))
            transform.parent = handGrip;    
        ResetTransform();
    }

    public void ResetTransform()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //transform.localScale = Vector3.one;
    }

    public void ActiveTrigger(int nya)
    {
        if (nya == 1f)
        {
            bx.enabled = true;
            Trail.SetActive(true);
            //ren.material = SwordOn;
        }
        else
        {
            bx.enabled = false;
            Trail.SetActive(false);
            //ren.material = SwordOff;
        }
    }
}
