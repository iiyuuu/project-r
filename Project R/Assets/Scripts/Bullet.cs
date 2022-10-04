using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool hitEnemy = false;
    public SpriteRenderer spriteRenderer;
    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && !enemy.enemyHurt)
            {
                hitEnemy = true;
                IEnumerator coroutine = enemy.Damaged();
                if (coroutine != null) { StopCoroutine(coroutine); }
                enemy.rb.velocity = Vector2.zero;
                enemy.Health -= 1;
                spriteRenderer.enabled = false;
                yield return StartCoroutine(enemy.Damaged());
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
}
