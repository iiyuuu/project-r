using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Enemy slime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Enemy") && !collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Help");
            spawnSlime(gameObject.transform.position);
            Erase();
        }

    }

    public void Erase()
    {
        Destroy(gameObject);
    }

    public void spawnSlime(Vector3 position)
    {
        Instantiate(slime).transform.position = position;
    }
}
