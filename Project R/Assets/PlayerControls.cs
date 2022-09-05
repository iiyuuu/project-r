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


    public float moveSpeed = 5.0f;
    private float activeMoveSpeed;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    

    //Vector 2 -> 2d Vector with X and Y speed

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        activeMoveSpeed = moveSpeed;
    }
    //Per Frame basis
    void Update()
    {
        FixedUpdate();
    }
    void FixedUpdate()
    {

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
        body.MovePosition(body.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private bool PlayerMovement(Vector2 direction)
    {
        int count = body.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if(count == 0)
        {
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;

            //No Collisions
            body.MovePosition(body.position + moveVector);
            return true;
        }
        else
        {
            //prints collisions; just debugging
            foreach(RaycastHit2D hit in castCollisions)
            {
                print(hit.ToString());
            }

            return false;
        }
    }

    void OnDash(InputValue value)
    {
        StartCoroutine(Dash());
   
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        body.velocity = new Vector2(body.velocity.x * dashingPower, body.velocity.x * dashingPower);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }




}
