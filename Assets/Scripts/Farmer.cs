using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Farmer : Human
{
    
    public int productFoodInterval = 1;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        // ABSTRACTION
        InvokeRepeating("ProductFood", 0, productFoodInterval);
    }

    void ProductFood()
    {
        GameManager.instance.food += 1;
    }

    // POLYMORPHISM
    public override void  Celebrate()
    {
        Debug.Log("Farmer Celebrate");
        CancelInvoke("ProductFood");
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
        Debug.Log("Farmer StopWorking");
        CancelInvoke("ProductFood");
    }
}
