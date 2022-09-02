using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D body;
    public float moveSpeed = 20.0f;
    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = 0.5f;
    public float dashCooldown = 1f;
    private Vector2 moveInput;

    private float dashCounter;
    private float dashCooldownTime;


    float moveLimiter = 0.85f;
    

    //Vector 2 -> 2d Vector with X and Y speed

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        activeMoveSpeed = moveSpeed;
    }
    //Per Frame basis
    void Update()
    {
        PlayerMovement();
        FixedUpdate();
        PlayerDash();
    }

    void FixedUpdate()
    {
        if(moveInput.x != 0 && moveInput.y != 0)//check if the sprite is moving diagonally
        {
            moveInput.x *= moveLimiter;
            moveInput.y *= moveLimiter;
        }

        body.velocity = new Vector2(moveInput.x * activeMoveSpeed, moveInput.y * activeMoveSpeed);
    }

    private void PlayerMovement()
    {

        bool isIdle = false;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        if(moveInput.x == 0 && moveInput.y == 0)
        {
            isIdle = true;
        }


        if (isIdle)
        {
            //play animation
        }
        else
        {
            body.velocity = moveInput * moveSpeed;
        }
    }

    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCooldownTime <=0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }

        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownTime = dashCooldown;
            }
        }

        if(dashCooldownTime > 0)
        {
            dashCooldownTime -= Time.deltaTime;
        }
    }



}
