using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHeadLook : MonoBehaviour
{
    public GameObject target;
    private Animator myAnimator;
    private bool lineOfSight;
  
    void Start()
    {
        SetInitialReferences(); 
    }

    void SetInitialReferences()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lineOfSight == false)
            lineOfSight = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (lineOfSight == true)
            lineOfSight = false;
    }

    void OnAnimatorIK()
    {
        if (myAnimator.enabled)
        {
            if (lineOfSight == true && target!=null)
            {
                myAnimator.SetLookAtWeight(1, 0, 0.5f, 0, 0.7f);
                myAnimator.SetLookAtPosition(target.transform.position);
            }
            else
            {
                myAnimator.SetLookAtWeight(0);
            }
        }
    }
}
