using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Human
{
    // Start is called before the first frame update
    public int productFoodInterval = 1;
    /*private Animator animator;*/
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("ProductFood", 0, productFoodInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ProductFood()
    {
        GameManager.instance.food += 1;
    }

    public override void  Celebrate()
    {
        Debug.Log("Farmer Celebrate");
        CancelInvoke("ProductFood");
        animator.Play("Celebrate");
    }

    public override void StopWorking()
    {
        Debug.Log("Farmer StopWorking");
        CancelInvoke("ProductFood");
    }
}
