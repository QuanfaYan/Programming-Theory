using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Human
{
    /*private Animator animator;*/
    public int buildInterval = 1;
    LineManage locateLine;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        locateLine = transform.parent.parent.GetComponent<LineManage>();
        InvokeRepeating("Build", 0, buildInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*    public override void OnAttack(GameObject enemy, int damage)
    {
        if (animator)
        {
            animator.Play("BuilderHit");
        }
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }*/

    private void Build()
    {
        if(locateLine)
        {
            locateLine.UpdateProgress(1);
        }
    }

    public override void Celebrate()
    {
        Debug.Log("Builder Celebrate");
        CancelInvoke("Build");
        animator.Play("Celebrate");
    }

    public override void StopWorking()
    {
        Debug.Log("Builder StopWorking");
        CancelInvoke("Build");
    }
}
