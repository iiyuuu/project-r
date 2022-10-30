using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth;
    public int currentHealth;
    public int maxAmmo;
    public int currentAmmo;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHeal;

    [Header("Damage Frames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    public bool hurt = false;
    
    
    private SpriteRenderer spriteRend;
    public GameObject UIRender;
    public Animator animator;
    public PlayerControls playerControls;
    public LevelLoader loader;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        currentAmmo = maxAmmo;
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

            Canvas[] canvases = FindObjectsOfType<Canvas>(true);
            foreach(Canvas c in canvases)
            {
                if (c.CompareTag("Respawn"))
                {
                    continue;
                }
                c.gameObject.SetActive(false);
            }


            SceneManager.LoadSceneAsync("Death");
            animator.SetTrigger("Death");
            //move to death scene
            //turn off everything except player
            //play animation after transition to scene
            //fade to hub
            //fade out or game over scene
        }
    }

    void Respawn()
    {
        loader = FindObjectOfType<LevelLoader>();

        StartCoroutine(loader.LoadingLevel("Hub"));
        animator.SetTrigger("Respawn");

        Canvas[] canvases = FindObjectsOfType<Canvas>(true);
        foreach (Canvas c in canvases)
        {
            c.gameObject.SetActive(true);
        }
        currentHealth = maxHealth;
        playerControls.canMove = true;
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