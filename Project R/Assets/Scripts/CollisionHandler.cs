using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public PlayerStats stats;
    public CurrencyManager currency;
    public MeleeController hitbox;
    public PlayerControls controls;

    IEnumerator coroutine;

    public float thrust;
    [SerializeField] public float kbTime = 0.2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag.Equals("Enemy"))
       {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * thrust;
            if (controls.canDash && !stats.hurt)
            {
                coroutine = kbCoroutine(controls.body);
                if(coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = other.GetComponent<Enemy>().Damaged();
                if(coroutine != null)
                {
                    StopCoroutine(coroutine);
                }

                stats.DamageTaken(1);
                
                controls.canMove = false;   

                controls.body.velocity = Vector2.zero;
                if (!controls.isMoving) { difference = (controls.body.mass * difference) / Time.fixedDeltaTime; }
                else { difference *= 3; }
                Debug.Log(difference);
                controls.body.AddForce(-difference, ForceMode2D.Impulse);
                StartCoroutine(kbCoroutine(controls.body));
                
                controls.canMove = true;
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

        if (other.gameObject.tag.Equals("Projectile Power Up"))
        {
            //stats.projectilePowerUp += 5;
            Destroy(other.gameObject);
        }

    }

    public IEnumerator kbCoroutine(Rigidbody2D tag)
    {
            yield return new WaitForSeconds(kbTime);
            tag.velocity = Vector2.zero;
    }



}
