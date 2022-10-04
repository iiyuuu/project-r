using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public Transform firePoint;
    public float attackRadius;
    public RangedAttack rangedAttack;

    public GameObject bulletPrefab;
    public float fireForce;
    public float firingCooldown;

    bool isFiring = false;

    // Start is called before the first frame update

    // Update is called once per frame

    public new void Update()
    {
    }
    private void FixedUpdate()
    {
        if (!enemyHurt)
        {
            CheckDistance();
        }
    }
    public override void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius)//if player is in aggro range
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, (-1)*moveSpeed * Time.fixedDeltaTime);
            if(transform.position.x > target.position.x) { spriteRend.flipX = true; }
            else { spriteRend.flipX = false;}
        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius && Vector2.Distance(target.position, transform.position) < attackRadius)
        {
            //coroutine, trigger, coroutine, trigger
            Vector2 difference = target.position - transform.position;
            float aimAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
            rangedAttack.body.rotation = aimAngle;

            if (!isFiring)
            {
                animator.SetTrigger("attack");
                StartCoroutine(FiringCooldown());
                //make enemy projectile prefab with dmg values
            }
            
            
            
            if (target.position.x > transform.position.x) { spriteRend.flipX = true; }
            else { spriteRend.flipX = false; }
        }
        else
        {
            Invoke("MoveBack", 1f);
        }

    }

    public IEnumerator FiringCooldown()
    {
        
        isFiring = true;
        yield return new WaitForSeconds(firingCooldown);
        isFiring = false;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        Destroy(bullet, 5);
    }


}
