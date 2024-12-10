using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Builder : Human
{
    public int buildInterval = 1;
    LineManage locateLine;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        locateLine = transform.parent.parent.GetComponent<LineManage>();
        // ABSTRACTION
        InvokeRepeating("Build", 0, buildInterval);
    }


    private void Build()
    {
        if(locateLine)
        {
            locateLine.UpdateProgress(1);
        }
    }

    // POLYMORPHISM
    public override void Celebrate()
    {
        Debug.Log("Builder Celebrate");
        CancelInvoke("Build");
        if(animator)
        {
            animator.Play("Celebrate");
            animator.SetBool("isWin", true);
        }
        else
        {
            Debug.Log("empty animator");
        }
    }

    // POLYMORPHISM
    public override void StopWorking()
    {
        Debug.Log("Builder StopWorking");
        CancelInvoke("Build");
    }
}
