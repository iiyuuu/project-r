using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    public GameObject fireballPrefab;
    public int segments;
    Rigidbody2D body;
    Vector2 targetPos;


    //shoot at them after animation
    //random spawn point for this object  //ave list of possible spawns to shoot at player
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        targetPos = (Vector2)GameObject.FindGameObjectWithTag("Player").transform.position;
        //play animation
    }

    public void Shoot()
    {
        for(int i = 1; i <= segments; i++)
        {       
            body.rotation = 360f / i;
        }

        GameObject bullet = Instantiate(fireballPrefab, transform.position, transform.rotation);
        bullet.GetComponentInChildren<Rigidbody2D>().AddForce(transform.up * 4, ForceMode2D.Impulse);
        Destroy(bullet, 3);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
