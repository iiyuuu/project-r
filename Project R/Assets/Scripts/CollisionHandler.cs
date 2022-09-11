using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public PlayerStats stats;
    public CurrencyManager currency;

    private void OnTriggerEnter2D(Collider2D other)
    {
       //if(other.gameObject.tag.Equals("Enemy"))
       //{
       //     Debug.Log("Enemy Hit you");
       //     stats.DamageTaken(1);
       //}
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



}
