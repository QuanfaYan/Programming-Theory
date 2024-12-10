using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public int cost;
    public int health;
    public GameObject locatedGrid;
    public bool isAttacked = false;
    public event System.Action OnDestroyed;
    protected Animator animator;

    private void OnDestroy()
    {
        if(null != locatedGrid)
        {
            locatedGrid.GetComponent<Grid>().isOccupied = false;
        }
        OnDestroyed?.Invoke();
    }

    public void OnAttack(GameObject enemy, int damage)
    {
        if (animator)
        {
            animator.Play("Hit");
        }
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Celebrate()
    {
        Debug.Log("Human Celebrate");
        if (animator)
        {
            animator.Play("Celebrate");
        }
    }

    public virtual void StopWorking()
    {
        Debug.Log("Human StopWorking");
        CancelInvoke();
    }

    public void Freeze()
    {
        animator.enabled = false;
    }
}
