using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class flyingEnemy : Enemy
{
    [Header("Attack Stuff")]
    public float chargeTime = 2f;
    public float dashingCooldown = 1.5f;
    public float dashForce;

    Vector2 moveVector;
    public Vector2 targetPosition;
    Collider2D hit;

    [Header("State")]
    public State currentState;
    public enum State
    {
        charging,
        dashing,
        idle,
        cooldown
    }

    public override void Start()
    {
        base.Start();
        currentState = State.idle;
    }

    protected override void FixedUpdate()
    {
        //check if target is in range
        //play charge animation
        //track player until fire
        //dash coroutine
        hit = Physics2D.OverlapCircle(transform.position, chaseRadius, LayerMask.GetMask("Player"));
        if((transform.right.x <= 0))
        {
            spriteRend.flipX = true;
        }
        else
        {
            spriteRend.flipX = false;
        }
        if (!enemyHurt)
        {
            switch (currentState)
            {
                case State.idle:
                    Charge();
                    break;
                case State.dashing:
                    if (Vector2.Distance((Vector2)transform.position, targetPosition) <= .1f || rb.velocity == Vector2.zero)//if eye reches target posiion (the player dodges it)
                    {
                        rb.velocity = Vector2.zero;
                        StartCoroutine(DashCooldown(dashingCooldown));
                    }
                    break;
                case State.cooldown:
                    rb.velocity = Vector2.zero;
                    break;
                case State.charging:
                    transform.right = (Vector2)transform.position - pointerInput;
                    break;
            }
        }

    }


    public void Charge()
    {
        IEnumerator coroutine = Charging(chargeTime);
        if (hit == null)//if player walks out of range
        {
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            currentState = State.idle;
            return;
        }
        StartCoroutine(coroutine);

    }

    public void Dash()
    {
        transform.right = (Vector2)transform.position - pointerInput;
        targetPosition = pointerInput;
        moveVector = (-(Vector2)transform.position + pointerInput).normalized;
        targetPosition = moveVector * 1.25f + (Vector2)transform.position;//past the target 

        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, moveVector, Vector2.Distance((Vector2)transform.position, targetPosition), LayerMask.GetMask("Interactable"));
        if (hit.collider != null)
        {
            targetPosition = hit.transform.position;
        }
        currentState = State.dashing;
        rb.AddForce(moveVector * dashForce, ForceMode2D.Impulse);

    }

    IEnumerator Charging(float chargeTime)
    {
        currentState = State.charging;
        yield return new WaitForSeconds(chargeTime);
        Dash();
    }

    IEnumerator DashCooldown(float dashCooldown)
    {
        currentState = State.cooldown;
        yield return new WaitForSeconds(dashCooldown);
        currentState = State.idle;

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, .05f);
        Debug.DrawRay(transform.position, moveVector);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(DashCooldown(dashingCooldown));//start cooldown on collision
        }

    }

}
