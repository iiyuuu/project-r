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
    private Vector2 dashDirection;

    public float baseMoveSpeed = 5.0f;
    private float activeMoveSpeed;

    IEnumerator dashCoroutine;
    public float dashingPower = 24f;
    bool isDashing;
    bool canDash = true;

    

    //Vector 2 -> 2d Vector with X and Y speed

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        activeMoveSpeed = baseMoveSpeed;

    }
    //input
    void Update()
    {
        FixedUpdate();
        //checks for space bar input then starts coroutine
        if (Input.GetKeyDown(KeyCode.Space) && canDash == true)
        {
            if (dashCoroutine != null)
            {
                //stop condition for coroutine if you are already dashing
                StopCoroutine(dashCoroutine);
            }
            dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(dashDirection == Vector2.zero)
            {
                dashDirection = new Vector2(transform.localScale.x, 0);
            }
            dashCoroutine = Dash(0.1f, 0.5f);
            StartCoroutine(dashCoroutine);
        }

    }
    //movement
    void FixedUpdate()
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
        if (!success)
        {
            success = PlayerMovement(new Vector2(moveInput.x, 0));
            if (!success)
            {
                success = PlayerMovement(new Vector2(0, moveInput.y));
            }
        }
        
    }
    private void OnMove(InputValue value)
    {
        //gathers user movement inputs
        moveInput = value.Get<Vector2>();
    }

    void physicsUpdate()
    {
        body.MovePosition(body.position + moveInput * activeMoveSpeed * Time.fixedDeltaTime);
    }

    private bool PlayerMovement(Vector2 direction)
    {
        int count = body.Cast(
            direction,
            movementFilter,
            castCollisions,
            activeMoveSpeed * Time.fixedDeltaTime + collisionOffset);

        if(count == 0)
        {
            Vector2 moveVector = direction * activeMoveSpeed * Time.fixedDeltaTime;

            //No Collisions
            body.MovePosition(body.position + moveVector);
            return true;
        }
        else
        {
            //prints collisions; just debugging
            foreach(RaycastHit2D hit in castCollisions)
            {
                print("hit");
            }

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




}
