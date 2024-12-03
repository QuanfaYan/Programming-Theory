using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    private Monster monster;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponentInParent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter" + other.gameObject.ToString());
        if(other.GetComponent<Human>())
        {
            //Debug.Log("Human");
            if(monster)
            {
                monster.StartAttack(other.gameObject);
            }
        }
        else if(other.GetComponent<Monster>())
        {
            //Debug.Log("Monster");
            if (monster)
            {
                monster.Wait(other.gameObject);
            }
        }
        else
        {
            //Debug.Log("otherthing");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null || other.GetComponent<Monster>())
        {
            monster.StopAttack();
        }
    }

}
