using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //Vector 2 -> 2d Vector with X and Y speed
    public Vector2 speed = new Vector2(10, 10);
    public float inputX = Input.GetAxis("Horizontal");
    public float inputY = Input.GetAxis("Vertical");
    public current_speed = 2.0f;

    //Per Frame basis
    void Update()
    { 
        //Vector 3 to contain the total velocity
        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);
    }

}
