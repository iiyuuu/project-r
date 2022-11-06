using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Enemy") && !collision.gameObject.tag.Equals("Player"))
        {
            Erase();
        }
    }

    public void Erase()
    {
        Destroy(gameObject);
    }
}
