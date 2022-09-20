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
            if (controls.canDash && !stats.hurt && !hitbox.meleeCollider.enabled)
            { 
                stats.DamageTaken(1);
                controls.canMove = false;
                controls.isMoving = false;
                enemy.isKinematic = false;

                difference = transform.position - enemy.transform.position;
                difference = difference.normalized * thrust;
                Debug.Log(difference);

                controls.body.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(kbCoroutine(controls.body));
                enemy.isKinematic = true;
                controls.canMove = true;
             
            }
            
            if(enemy != null && hitbox.meleeCollider.enabled)
            {
                enemy.isKinematic = false;
                difference /= 2;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(kbCoroutine(enemy));
                enemy.isKinematic = true;
                //add enemy flash red here
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
       if (other.gameObject.tag.Equals("Health Power Up"))
        {
            Debug.Log("Health Power Up Picked Up");
            stats.maxHealth += 2;
            stats.Healing(2);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag.Equals("Speed Power Up"))
        {
            Debug.Log("Speed Power Up Picked Up");
            controls.baseMoveSpeed += 5;
            controls.activeMoveSpeed += 5;
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
