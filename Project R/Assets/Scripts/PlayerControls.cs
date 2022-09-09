using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    private Rigidbody2D body;
    private Vector2 moveInput;

    public float baseMoveSpeed = 50f;
    private float activeMoveSpeed;
    public float idleFriction = 0.9f;

    IEnumerator dashCoroutine;
    public float dashingPower = 24f;
    bool isDashing;
    bool canDash = true;
    private Vector2 dashDirection;

    Animator animator;
    SpriteRenderer spriteRenderer;

    public PlayerCombat combat;

    //Vector 2 -> 2d Vector with X and Y speed

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();//added some animation precode
        spriteRenderer = GetComponent<SpriteRenderer>();//sprite render for flipping sprite (no need for both left and right sprite now)
        activeMoveSpeed = baseMoveSpeed;

    }
    //input
    void Update()
    {
        FixedUpdate();
        //checks for space bar input then starts coroutine

    }
    //movement
    void FixedUpdate()
    {

        //movement speed modifier
        if (isDashing)
        {
            print(dashDirection);
            body.velocity = dashDirection.normalized * dashingPower;
            return;
        }

        // Try to move player in input direction, followed by left right and up down input if failed
        bool success = PlayerMovement(moveInput);

        //if player hits a wall check if player can move left/right or up/down the wall
        if(moveInput != Vector2.zero)
        {
            if (!success)
            {
                success = PlayerMovement(new Vector2(moveInput.x, 0));
                if (!success)
                {
                    success = PlayerMovement(new Vector2(0, moveInput.y));
                }
            }

            //flips sprite if you are moving left/right
            if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            UpdateAnimationParameters();
            //animator.SetBool("isMoving", success);
        }
        else
        {
            body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, 1);
            //animator.SetBool("isMoving", false);
        }
        
        
    }
    private void OnMove(InputValue value)
    {
        //gathers user movement inputs
        moveInput = value.Get<Vector2>();
    }

    private bool PlayerMovement(Vector2 direction)
    {
            int count = body.Cast(
            moveInput,
            movementFilter,
            castCollisions,
            activeMoveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                Vector2 moveVector = moveInput * activeMoveSpeed * Time.fixedDeltaTime;

            //No Collisions
                body.velocity = Vector2.ClampMagnitude(body.velocity + moveVector, activeMoveSpeed);
                //body.MovePosition(body.position + moveVector);
                return true;
            }
            else
            {
                return false;
            }
            
    }

    private IEnumerator Dash(float dashingTime, float dashingCooldown)
    {
        canDash = false;
        isDashing = true;
        yield return new WaitForSeconds(dashingTime);//during dash
        isDashing = false;
        activeMoveSpeed = baseMoveSpeed;
        yield return new WaitForSeconds(dashingCooldown);//wait dash cd
        canDash = true;
    }

    void OnDash()
    {
        if (canDash)
        {
            if (dashCoroutine != null)
            {
                //stop condition for coroutine if you are already dashing
                StopCoroutine(dashCoroutine);
            }

            if (moveInput == Vector2.zero)
            {
                if (spriteRenderer.flipX && moveInput.y == 0)//if sprite is facing left
                {
                    dashDirection = new Vector2(-transform.localScale.x, 0);//move in set y direction
                }
                else if (!spriteRenderer.flipX && moveInput.y == 0)//if sprite is facing right
                {
                    dashDirection = new Vector2(transform.localScale.x, 0);//move in set x direction
                }
            }
            else
            {
                dashDirection = new Vector2(moveInput.x, moveInput.y);//will dash in diagonal movement
            }
            dashCoroutine = Dash(0.1f, 0.5f);//calls coroutine for dash manuever
            StartCoroutine(dashCoroutine);
        }
        else
        {
            return;
        }
    }

    void UpdateAnimationParameters()
    {
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);
    }
}
