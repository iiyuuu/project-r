using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;
    public int projectilePowerUp = 0;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHeal;

    [Header("damageFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] public bool hurt = false;
    private SpriteRenderer spriteRend;
    public GameObject UIRender;

    public Animator animator;
    public PlayerControls playerControls;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        projectilePowerUp = 0;
    }

    public void DamageTaken(int amount)
    {
        currentHealth -= amount;
        OnPlayerDamaged?.Invoke();
        StartCoroutine(Invulnerabilty());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnPlayerDeath?.Invoke();
            playerControls.canMove = false;
            animator.SetTrigger("Death");
            //spriteRend.sortingOrder = UIRender.sortingOrder + 1;
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

    private IEnumerator Invulnerabilty()
    {
        hurt = true;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0,0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));

        }
        hurt = false;
    }
}