using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingEnemy : Enemy
{
    [Header("Attack Stuff")]
    public float chargeTime = 2f;
    public Transform firePoint;

    private bool lockedOn = false;

    float dashingCooldown = 1.5f;

    public float dashingTime;

    public LayerMask layerMask;
    public RaycastHit2D hit;

    public enum State
    {
        charging,
        dashing,
        idle
    }

    public void Update()
    {
        if (firePoint.rotation.eulerAngles.z > 179) { spriteRend.flipY = true; }
        else { spriteRend.flipY = false; }
    }

    protected override void FixedUpdate()
    {
        //check if target is in range
        //play charge animation
        //track player until fire
        //dash coroutine
        if (!enemyHurt)
        {
            Charge();

        }


    }


    public void Charge()
    {
        if (canRotate && canDash)
        {
            hit = Physics2D.Raycast(firePoint.position, pointerInput, layerMask);
            if (hit)
            {
                canRotate = false;
            }
            else
            {
                transform.Rotate(transform.forward);
            }
        }
        else
        {
            //Vector3 temp = new Vector3(0, -offset, 0);
            if (lockedOn && canDash && !isDashing)
            {
                if(Vector3.Distance(hit.collider.transform.position, transform.position) > 1.75f)
                {
                    rb.AddForce((hit.collider.transform.position - transform.position) * 1.25f, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce((hit.collider.transform.position - transform.position) * 3.25f, ForceMode2D.Impulse);
                }
  
      
                isDashing = true;
                //if it reaches a distance call cooldown
                //maybe do movetowards instead of add force
                StartCoroutine(DashCooldown());
                lockedOn = false;
                canRotate = true;


            }
            else
            {
                
                lockedOn = true;
            }

        }
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(dashingCooldown);//wait dash cd
        
        canDash = true;
    }


}
