using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool hitEnemy = false;
    public SpriteRenderer spriteRenderer;
    public float kbPower = 1f;
    public Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hitEnemy)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator kbCoroutine(Rigidbody2D tag, float kbTime)
    {
        rb.velocity = Vector2.zero;
        if(tag != null)
        {
            yield return new WaitForSeconds(kbTime);
            tag.velocity = Vector2.zero;
            Destroy(gameObject);
        }
        else { yield return null; }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
