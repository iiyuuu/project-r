using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public PlayerStats stats;
    public CurrencyManager currency;
    public MeleeHitbox hitbox;
    public PlayerControls controls;

    public float thrust;
    [SerializeField] public float kbTime = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag.Equals("Enemy"))
       {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * thrust;
            Debug.Log(difference);
            if (controls.canDash && !stats.hurt && !hitbox.meleeCollider.enabled)
            { 
                stats.DamageTaken(1);
                
                controls.canMove = false;

                controls.body.AddForce(-difference, ForceMode2D.Impulse);
                StartCoroutine(kbCoroutine(controls.body));
                
                controls.canMove = true;
            }
            
            if(enemy != null && hitbox.meleeCollider.enabled)
            {
                difference = difference / 2;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(kbCoroutine(enemy));
            }
       }
       if (other.gameObject.tag.Equals("Heal"))
       {
           stats.Healing(2);
           Destroy(other.gameObject);
       }
       if (other.gameObject.tag.Equals("Coin"))
       {
            currency.ChangeCurrency(1);
            Destroy(other.gameObject);
       }
       if (other.gameObject.tag.Equals("Health Power Up"))
        {
            //Debug.Log("Health Power Up Picked Up");
            stats.maxHealth += 2;
            stats.Healing(2);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag.Equals("Speed Power Up"))
        {
            //Debug.Log("Speed Power Up Picked Up");
            controls.baseMoveSpeed += 3;
            controls.activeMoveSpeed += 3;
            Destroy(other.gameObject);
        }

    }

    private IEnumerator kbCoroutine(Rigidbody2D tag)
    {
            yield return new WaitForSeconds(kbTime);
            if(tag != null)
            {
                tag.velocity = Vector2.zero;
            }
    }



}
