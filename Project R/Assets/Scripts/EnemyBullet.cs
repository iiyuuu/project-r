using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(0);
        }
        
    }
}
