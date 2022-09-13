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
    public static event Action OnPlayerHeal;

    [Header("damageFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    public bool hurt = false;

    


    void Start()
    {
        currentHealth = maxHealth;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void DamageTaken(int amount)
    {
        currentHealth -= amount;
        OnPlayerDamaged?.Invoke();
        StartCoroutine(invulnerabilty());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnPlayerDeath?.Invoke();
            Debug.Log("dejj");
            //death animation
            //fade out or game over scene
        }
    }

    public void Healing(int amount)
    {
        currentHealth += amount;
        OnPlayerHeal?.Invoke();

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private IEnumerator invulnerabilty()
    {
        hurt = true;
        Physics2D.IgnoreLayerCollision(6,7,true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0,0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes*2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));

        }
        hurt = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}