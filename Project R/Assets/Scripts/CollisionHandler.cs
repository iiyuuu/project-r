using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public PlayerStats stats;
    public CurrencyManager currency;

    IEnumerator damageCoroutine;
    private bool canDamage = true;
    private bool isDamaged = false;
    private float damageCooldown = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag.Equals("Enemy"))
       {
            if (canDamage)
            {
                if(damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                }
                else
                {
                    Debug.Log("Enemy Hit you");
                    stats.DamageTaken(1);
                    StartCoroutine(damageCoroutine);
                }
            }

       }
       if (other.gameObject.tag.Equals("Heal"))
       {
           Debug.Log("Grabbed Heart");
           stats.Healing(2);
           Destroy(other.gameObject);
       }
       else if (other.gameObject.tag.Equals("Coin"))
       {
            Debug.Log("Grabbed Coin");
            currency.ChangeCurrency(1);
            Destroy(other.gameObject);
       }

    }

    private IEnumerator playerTakeDamage()
    {
        canDamage = false;
        isDamaged = true;
        yield return new WaitForSeconds(damageCooldown); //I-frames
        isDamaged = false;
        canDamage = true;
    }



}
