using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    GameObject objectPrefab;
    public Transform firepoint;
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            //activate trap
            GameObject trapObject = Instantiate(objectPrefab, firepoint);//spawns trap object
            if (trapObject.CompareTag("Trap"))
            {
                Transform targetPosition = 
            }
            
        }
    }

}
