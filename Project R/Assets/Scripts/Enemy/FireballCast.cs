using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireballCast : MonoBehaviour
{
    public GameObject fireballPrefab;
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
        Vector2 difference = targetPos - (Vector2)transform.position;
        float aimAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;//aiming code
        body.rotation = aimAngle;

        GameObject bullet = Instantiate(fireballPrefab, transform.position, transform.rotation);
        bullet.GetComponentInChildren<Rigidbody2D>().AddForce(transform.up * 4, ForceMode2D.Impulse);
        Destroy(bullet, 3);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
