using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] protected int health = 10;
    [SerializeField] int power = 2;
    [SerializeField] float speed = 0.5f;
    [SerializeField] bool m_isMoving = true;
        
    public bool isMoving
    {
        get 
        {
            return m_isMoving; 
        }
        set
        {
            if (animator)
            {
                animator.SetBool("isMoving", value);
            }
            m_isMoving = value;
        }
    }
    [SerializeField] int attackInterval = 1;
    private Human attackTarget = null;
    IEnumerator attackRoutine = null;
    private Monster waitTarget = null;
    public event System.Action OnDestroyed;
    private Grid locateGrid = null;
    protected Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMoving)
        {
            Move();
        }
    }

    void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if(bullet != null)
        {
            HitByBullet(bullet);
        }
        else if(other.GetComponent<Laser>())
        {
            if(animator)
            {
                animator.SetBool("isLiving", false);
            }

            StartCoroutine("DestroyCoroutine");
        }
        else
        {
            Grid grid = other.GetComponent<Grid>();
            if(grid != null)
            {
                locateGrid = grid;
            }
        }
    }
    private void OnDestroy()
    {
        if(attackTarget)
        {
            attackTarget.OnDestroyed -= StopAttack;
        }

        if(waitTarget)
        {
            waitTarget.OnDestroyed -= StopAttack;
        }

        if(locateGrid)
        {
            locateGrid.isOccupied = false;
        }

        OnDestroyed?.Invoke();
    }
    public int GetPower()
    {
        return power;
    }
    
    public void Wait(GameObject target)
    {
        waitTarget = target.GetComponent<Monster>();
        if(waitTarget)
        {
            waitTarget.OnDestroyed += StopAttack;
        }
        isMoving = false;
    }

    public void StartAttack(GameObject target)
    {
        attackTarget = target.GetComponent<Human>();
        attackRoutine = AttackCoroutine();
        StartCoroutine(attackRoutine);
        isMoving = false;
    }

    public void StopAttack()
    {
        attackTarget = null;
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }
        isMoving = true;
    }

    IEnumerator AttackCoroutine()
    {
        if(attackTarget)
        {
            attackTarget.OnDestroyed += StopAttack;
            while(true)
            {
                Attack();
                yield return new WaitForSeconds(attackInterval);
            }
        }
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void Attack()
    {
        if(animator)
        {
            //animator.SetTrigger("attack");
            animator.Play("Attack01");
        }
        attackTarget.OnAttack(gameObject, power);
    }

    public void StopWorking()
    {
        isMoving = false;
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }
    }

    public void Celebrate()
    {
        //Debug.Log("Monster Celebrate");
        if(animator)
        {
            animator.Play("Victory");
        }
    }

    public virtual void HitByBullet(Bullet bullet)
    {
        if (animator)
        {
            animator.Play("GetHit");
        }
        health -= bullet.GetBulletPower();
        if (health <= 0)
        {
            if (animator)
            {
                animator.SetBool("isLiving", false);
            }
            Destroy(gameObject);
        }
        Destroy(bullet.gameObject);
    }
}
