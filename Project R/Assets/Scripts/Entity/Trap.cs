using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform[] firepoint;
    
    [Range(-5,5)]
    public float force;
    public bool horizontal;
    public bool vertical;

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
            foreach(Transform f in firepoint)
            {
                GameObject trapObject = Instantiate(objectPrefab, f.position, f.rotation);//spawns trap object
                if (trapObject.CompareTag("Trap"))
                {
                    if (horizontal)
                    {
                        if(force <= 0)
                        {
                            sprite.flipX = true;
                        }
                        trapObject.GetComponent<Rigidbody2D>().AddForce(f.right * force, ForceMode2D.Impulse);
                    }
                    else if (vertical)
                    {
                        //Z + 90 = down
                        if(force <= 0)
                        {
                            trapObject.transform.Rotate(0, 0, 90f);
                        }
                        //Z - 90 = up
                        else
                        {
                            trapObject.transform.Rotate(0, 0, -90f);
                        }
                        trapObject.GetComponent<Rigidbody2D>().AddForce(f.up * force, ForceMode2D.Impulse);
                    }
                    Destroy(trapObject, 2f);
                }
            }
            
            
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sprite.sprite = newSprite;
        }
        
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sprite.sprite = oldSprite;
        }
            
    }

}
