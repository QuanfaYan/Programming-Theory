using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyleArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bullet>())
        {
            Destroy(other.gameObject);
        }
    }
}
