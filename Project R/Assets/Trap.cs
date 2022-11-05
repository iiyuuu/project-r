using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform firepoint;
    
    [Range(-5,5)]
    public float force;

    [Header("Sprite")]
    public SpriteRenderer sprite;
    public Sprite oldSprite;
    public Sprite newSprite;
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            sprite.sprite = newSprite;
            //activate trap
            GameObject trapObject = Instantiate(objectPrefab, firepoint);//spawns trap object
            if (trapObject.CompareTag("Trap"))
            {
                trapObject.GetComponentInChildren<Rigidbody2D>().AddForce(firepoint.right * force, ForceMode2D.Impulse);
            }
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        sprite.sprite = oldSprite;
    }

}
