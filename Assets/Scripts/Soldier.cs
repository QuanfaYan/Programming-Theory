using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Human
{
    [SerializeField] float fireInterval;
    [SerializeField] float firePower;
    private Vector3 bulletRelativePos = new Vector3(0.741684f, 0.425f, -0.183531f);
    /*private Animator animator;*/
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("Fire", 0, fireInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spwan bullet shoot enenmy
    void Fire()
    {
        //Debug.Log(locatedGrid.ToString() + " fire:" +  transform.position.ToString());
        Instantiate(bulletPrefab, transform.position + bulletRelativePos, bulletPrefab.transform.rotation);

        if(animator)
        {
            animator.Play("Fire");
        }
    }

    public override void Celebrate()
    {
        Debug.Log("Soldier Celebrate");
        CancelInvoke("Fire");
        animator.Play("Celebrate");
    }

    public override void StopWorking()
    {
        Debug.Log("Soldier StopWorking");
        CancelInvoke("Fire");
    }
}
