using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtBarr : MonoBehaviour
{
    private Transform bar;
   private  void Start()
   {
         bar = transform.Find("Bar");
       
   }

    public void SetSize(float sizeNormalize)
    {
        bar.localScale = new Vector3(sizeNormalize, 1f,0);
    }

    
}
