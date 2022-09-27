using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && !enemy.enemyHurt)
            {
                enemy.Health -= 1;
                enemy.enemyHurt = true;
                StartCoroutine(enemy.Damaged());
                //add kb coroutine
                enemy.enemyHurt = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
