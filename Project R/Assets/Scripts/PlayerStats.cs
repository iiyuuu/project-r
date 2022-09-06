using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void DamageTaken(int amount)
    {
        currentHealth -= amount;
        //OnPlayerDamaged?.Invoke();

        if(currentHealth <= 0){
            currentHealth = 0;
            //OnPlayerDeath?.Invoke();
            Debug.Log("dejj");
            //dejj
            //death animation
            //fade out or game over scene
        }
    }

    void Healing(int amount)
    {
        currentHealth += amount;
        if (currentHealth <= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}