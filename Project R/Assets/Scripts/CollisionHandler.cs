using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public PlayerStats stats;
    public CurrencyManager currency;
    public PlayerControls controls;
    public MeleeHitbox hitbox;

    public float thrust;
    [SerializeField] public float kbTime = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag.Equals("Enemy"))
       {
            if (controls.canDash && !stats.hurt && !hitbox.meleeCollider.enabled)
            {
                //stats.DamageTaken(1);
            }
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if(enemy != null)
            {
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(kbCoroutine(enemy));
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

    private IEnumerator kbCoroutine(Rigidbody2D enemy)
    {
        if(enemy != null)
        {
            yield return new WaitForSeconds(kbTime);
            enemy.velocity = Vector2.zero;
        }
    }



}
