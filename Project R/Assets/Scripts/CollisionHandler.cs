using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : PlayerControls
{
    public PlayerStats stats;
    public CurrencyManager currency;
    public MeleeHitbox hitbox;

    public float thrust;
    [SerializeField] public float kbTime = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag.Equals("Enemy"))
       {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * thrust;
            if (canDash && !stats.hurt && !hitbox.meleeCollider.enabled)
            {
                //stats.DamageTaken(1);
                canMove = false;
                enemy.isKinematic = false;
                difference = transform.position - enemy.transform.position;
                difference = difference.normalized * thrust;
                Debug.Log(enemy.isKinematic);
                body.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(kbCoroutine(body));
                enemy.isKinematic = true;
                canMove = true;
            }
            
            if(enemy != null && hitbox.meleeCollider.enabled)
            {
                enemy.isKinematic = false;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(kbCoroutine(enemy));
                enemy.isKinematic = true;
            }
       }
       if (other.gameObject.tag.Equals("Heal"))
       {
           Debug.Log("Grabbed Heart");
           stats.Healing(2);
           Destroy(other.gameObject);
       }
       if (other.gameObject.tag.Equals("Coin"))
       {
            Debug.Log("Grabbed Coin");
            currency.ChangeCurrency(1);
            Destroy(other.gameObject);
       }

    }

    private IEnumerator kbCoroutine(Rigidbody2D tag)
    {
        if(tag != null)
        {
            yield return new WaitForSeconds(kbTime);
            tag.velocity = Vector2.zero;
        }
    }



}
