using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public Transform firePoint;
    public float attackRadius;
    public RangedAttack rangedAttack;

    public Vector2 hingeBase;

    public GameObject bulletPrefab;
    public float fireForce;
    public float fireRate;


    bool isFiring = false;

    // Start is called before the first frame update

    // Update is called once per frame
    public override void Start()
    {
        base.Start();
        hingeBase = rangedAttack.transform.localPosition;
    }

    protected override void FixedUpdate()
    {
        if (!enemyHurt)
        {
            if (spriteRend.flipX    )
            {
                rangedAttack.transform.localPosition = new Vector2(-hingeBase.x, hingeBase.y);
            }
            else
            {
                rangedAttack.transform.localPosition = hingeBase;
            }
            CheckDistance();
        }
    }
    public override void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius)//if player is in aggro range
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, (-1)*moveSpeed * Time.fixedDeltaTime);
            if(transform.position.x > target.position.x) { print("True");  spriteRend.flipX = true; }
            else { spriteRend.flipX = false; }
        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius && Vector2.Distance(target.position, transform.position) < attackRadius && !isFiring)
        {
            //coroutine, trigger, coroutine, trigger
            Vector2 difference = target.position - rangedAttack.transform.position;
            float aimAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
            rangedAttack.body.rotation = aimAngle;


            if (target.position.x > transform.position.x) { spriteRend.flipX = true; }//point at player
            else { spriteRend.flipX = false; }

            if (!isFiring)
            {
                animator.SetTrigger("attack");
                StartCoroutine(FiringCooldown());
            }
            
        }
        else
        {
            Invoke("MoveBack", 1f);
        }
        
    }

    public IEnumerator FiringCooldown()
    {
        
        isFiring = true;
        yield return new WaitForSeconds(1/ fireRate);
        isFiring = false;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        Destroy(bullet, 5);
    }


}
