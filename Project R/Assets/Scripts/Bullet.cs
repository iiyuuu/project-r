using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool hitEnemy = false;
    public SpriteRenderer spriteRenderer;
    IEnumerator coroutine;
    IEnumerator kbcoroutine;
    [SerializeField] private float launch = 3;
    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && !enemy.enemyHurt)
            {
                //kb part
                Rigidbody2D enemyBody = enemy.GetComponent<Rigidbody2D>();
                Vector2 difference = enemyBody.transform.position - transform.position;
                difference = difference.normalized * launch;

                enemyBody.velocity = Vector2.zero;
                enemyBody.AddForce(difference, ForceMode2D.Impulse);
                //coroutine
                if (kbcoroutine != null) { StopCoroutine(kbcoroutine); }
                kbcoroutine = kbCoroutine(enemyBody, 0.2f);
                StartCoroutine(kbcoroutine);

                //damage part
                hitEnemy = true;
                if (coroutine != null) { StopCoroutine(coroutine); }
                coroutine = enemy.Damaged();  
                enemy.rb.velocity = Vector2.zero;
                enemy.Health -= 1;
                spriteRenderer.enabled = false;
                yield return StartCoroutine(coroutine);
                Destroy(gameObject);

            }
        }
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
        yield return new WaitForSeconds(kbTime);
        tag.velocity = Vector2.zero;
    }
}
