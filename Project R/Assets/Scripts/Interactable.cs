using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode[] interactKey;
    public ShopManager shop;

    private void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shop").GetComponentInChildren<ShopManager>(true);
    }

    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey[0]))
            {
                shop.EnableShop();
            }
            else if (Input.GetKeyDown(interactKey[1]))
            {
                shop.DisableShop();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControls>().PromptEnable();
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControls>().PromptDisable();
            isInRange = false;
        }
    }
}
