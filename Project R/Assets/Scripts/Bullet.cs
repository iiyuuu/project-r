using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool hitEnemy = false;
    public SpriteRenderer spriteRenderer;

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
