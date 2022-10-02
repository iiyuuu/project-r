using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && !enemy.enemyHurt)
            {
                if (enemy.Damaged() != null) { StopCoroutine(enemy.Damaged()); }
                enemy.rb.velocity = Vector2.zero;
                enemy.Health -= 1;
                StartCoroutine(enemy.Damaged());
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
