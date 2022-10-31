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
    public float kbTime = 0.2f;

    public void Update()
    {
        currency = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<CurrencyManager>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Enemy Projectile"))
       {
            Enemy enemy = other.GetComponent<Enemy>();
            Vector2 difference = transform.position - enemy.rb.transform.position;
            difference = difference.normalized * thrust; //(1, 0) - (3, 0) = (-2, 0) -> (-1, 0) * thrust (3) = (-3, 0) force
            if (controls.canDash && !stats.hurt)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = kbCoroutine(controls.body);
               
                if(enemy.attackDamage <= 0)
                {
                    stats.DamageTaken(1);
                }
                else
                {
                    stats.DamageTaken(enemy.attackDamage);
                }
                
               
                //difference = (controls.body.mass * difference) / Time.fixedDeltaTime;
                Debug.Log(difference);
                controls.body.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(coroutine);
                
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
            controls.canMove = false;
            yield return new WaitForSeconds(kbTime);
            tag.velocity = Vector2.zero;
            controls.canMove = true;    
    }



}
