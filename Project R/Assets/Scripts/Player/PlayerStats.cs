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
    public int currency;
    public int attackDamage;

    public bool savedata;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerHeal;

    [Header("Damage Frames")]
    public bool hurt = false;
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;

    [Header("Borrowed Componments")]
    [SerializeField] private SpriteRenderer spriteRend;
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
    private void Update()
    {
        if (!savedata)
        {
            currency = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<CurrencyManager>(true).currency;
            attackDamage = GetComponentInChildren<MeleeController>(true).attackDamage;
        }
        
    }

    public void Save()
    {
        SaveManager.SavePlayer(this);
        savedata = true;
    }

    public void Load()
    {
        if (savedata)
        {
            PlayerData data = SaveManager.LoadPlayer();
            currency = data.currency;
            attackDamage = data.attackDamage;
            maxHealth = data.maxHealth;
            maxAmmo = data.maxAmmo;
        }
        else
        {
            return;
        }
        
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


            SceneManager.LoadScene("Death");
            animator.SetTrigger("Death");
            //move to death scene
            //turn off everything except player
            //play animation after transition to scene
            //fade to hub
            //fade out or game over scene
        }
    }

    IEnumerator Respawn()
    {
        loader = FindObjectOfType<LevelLoader>();

        yield return StartCoroutine(loader.LoadingLevel("Hub"));
        animator.SetTrigger("Respawn");

        Canvas[] canvases = FindObjectsOfType<Canvas>(true);
        foreach (Canvas c in canvases)
        {
            c.gameObject.SetActive(true);
        }
        currentHealth = maxHealth;
        playerControls.canMove = true;
        playerControls.floor_number = 1;
        playerControls.usedScenes.Clear();
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
        FindObjectOfType<AudioManager>().Play("Player Hurt");
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