using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    [SerializeField] public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    [SerializeField] public Rigidbody2D body;
    public Vector2 moveInput;

    public float baseMoveSpeed = 50f;
    [SerializeField] public float activeMoveSpeed;
    [SerializeField] public float idleFriction = 0.9f;

    IEnumerator dashCoroutine;
    public float dashingPower = 24f;
    [SerializeField] public bool isDashing;
    [SerializeField] public bool canDash = true;
    private Vector2 dashDirection;

    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] public bool canMove = true;
    [SerializeField] public bool isMoving = false;
    public MeleeHitbox melee;

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
        //checks for space bar input then starts coroutine

    }
    //movement
    void FixedUpdate()
    {
        if (canMove)
        { 
            //movement speed modifier
            if (isDashing)
            {

                body.velocity = dashDirection.normalized * dashingPower;
                return;
            }

            // Try to move player in input direction, followed by left right and up down input if failed
            bool success = PlayerMovement(moveInput);

            //if player hits a wall check if player can move left/right or up/down the wall
            if (moveInput != Vector2.zero)
            {
                isMoving = true;
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

                animator.SetBool("isMoving", success);
            }
            else
            {
                isMoving = false;
                animator.SetBool("isMoving", false);
                body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, 0.9f);
            }
        }
        
    }
    private void OnMove(InputValue value)
    {
        //gathers user movement inputs
        moveInput = value.Get<Vector2>();
    }

    private bool PlayerMovement(Vector2 direction)
    {
        if(moveInput != Vector2.zero)
        {
            int count = body.Cast(
            direction,
            movementFilter,
            castCollisions,
            activeMoveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                Vector2 moveVector = direction.normalized * activeMoveSpeed;

                //No Collisions
                //body.MovePosition(body.position + moveVector);
                body.AddForce(moveVector);
                //body.velocity = moveVector;
                return true;
            }
            else
            {
                return false;
            }
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
        Physics2D.IgnoreLayerCollision(6, 8, false);
        yield return new WaitForSeconds(dashingCooldown);//wait dash cd
        canDash = true;
    }

    void OnDash()
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);
        if (canDash)
        {
            animator.SetTrigger("isDashing");
            Debug.Log("Dash");
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
            dashCoroutine = Dash(0.2f, 0.5f);//calls coroutine for dash manuever
            StartCoroutine(dashCoroutine);
        }
        else
        {
            return;
        }
    }

    void OnMelee()
    { 
        animator.SetTrigger("isAttacking");
    }

    public void MeleeAttack()
    {
        LockMovement();
        
        if(spriteRenderer.flipX == true)
        {
            melee.AttackLeft();
        }
        else
        {
            melee.AttackRight();
        }
    }

    public void EndAttack()
    {
        UnlockMovement();
        melee.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
        body.velocity = Vector2.zero;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
