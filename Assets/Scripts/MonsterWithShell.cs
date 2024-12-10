using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class MonsterWithShell : Monster
{
    [SerializeField] private int shellHealth = 16;

    // POLYMORPHISM
    public override void HitByBullet(Bullet bullet)
    {
        if(shellHealth > 0)
        {
            if(animator)
            {
                animator.Play("DefendGetHit");
            }
            shellHealth -= bullet.GetBulletPower();
        }
        else
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
        }
        Destroy(bullet.gameObject);
    }
}
