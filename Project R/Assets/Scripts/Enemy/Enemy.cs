using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Stats")]
    public int health = 3;
    public float moveSpeed = 3f;
    public float chaseRadius;
    public bool ranged;

    [Header("Movement")]
    private Vector2 moveInput;
    private Vector2 pointerInput;
    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MoveInput { get => moveInput; set => moveInput = value; }
    public Vector3 homePosition;
    
    public Animator animator;

    [Header("Damage Indicator")]
    public float iFrameDuration;
    [SerializeField] int numberOfFlashes;
    public bool enemyHurt = false;
    public SpriteRenderer spriteRend;

    IEnumerator coroutine;
    IEnumerator kbCoroutine;


    public int Health
    {
        set
        {
            health = value;
            if(health <= 0)
            {
                animator.SetTrigger("enemyDeath");
                foreach (CircleCollider2D circle in gameObject.GetComponents<CircleCollider2D>())
                {
                    circle.enabled = false;
                }
            }
        }
        get
        {
            return health;
        }
    }
    

     public virtual void Start()
     {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        homePosition = transform.position;
     }

    protected virtual void FixedUpdate()
    {
        if (!enemyHurt && !ranged)
        {
            rb.velocity = MoveInput.normalized * moveSpeed;
            if (PointerInput.x > transform.position.x) { spriteRend.flipX = true; }
            else { spriteRend.flipX = false; }
            CheckDistance();
        }
    }

    public IEnumerator Damaged()
    {
        enemyHurt = true;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        enemyHurt = false;

    }
    
    public virtual void CheckDistance()
    {
        if(Vector2.Distance(PointerInput, transform.position) > chaseRadius && rb.velocity == Vector2.zero)
        {
            Invoke("MoveBack", 1f);//1s delay before moving back
        }
        
    }

    public void MoveBack()
    {
        Debug.Log("Moving Back");
        if (Vector2.Distance(PointerInput, transform.position) > chaseRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, homePosition, moveSpeed * Time.fixedDeltaTime);
            if(homePosition.x < transform.position.x)
            {
                spriteRend.flipX = false;
            }
            else if (homePosition == transform.position) 
            {
                spriteRend.flipX = false;
            }
            else
            {
                spriteRend.flipX = true;
            }
        }
        
    }

    void Death()
    {
        enemyHurt = true;
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Projectile"))
        {
            if(rb != null && !enemyHurt)
            {
                Bullet bullet = collision.GetComponent<Bullet>();

                //if(coroutine != null)
                //{
                //    StopCoroutine(coroutine);
                //}

                coroutine = Damaged();

                rb.velocity = Vector2.zero;
                Health -= 1;
                bullet.spriteRenderer.enabled = false;
                bullet.rb.velocity = Vector2.zero;
                bullet.GetComponent<CircleCollider2D>().enabled = false;

                kbCoroutine = bullet.kbCoroutine(rb, 0.2f);
                if(kbCoroutine != null && bullet != null)
                {
                    StopCoroutine(kbCoroutine);
                }
                if (rb != null && bullet.rb != null)
                {
                    Vector2 difference = rb.transform.position - bullet.rb.transform.position;
                    difference = difference.normalized * bullet.kbPower;
                    rb.AddForce(difference, ForceMode2D.Impulse);
                }
                bullet.rb = null;
                
                
                StartCoroutine(coroutine);
                StartCoroutine(kbCoroutine);
                
                
            }
        }
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            if (enemyHurt)//checks if enemy is hurt and hurts both objects
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                if (enemy != null)
                {

                    if (rb == null || enemy.rb == null)
                    {
                        return;
                    }
                    enemy.rb.velocity = Vector2.zero;
                    rb.velocity = Vector2.zero;
                }

                if (Health <= 0 || enemy.Health <= 0)
                {
                    StopAllCoroutines();
                    enemy.enemyHurt = false;
                    enemyHurt = false;
                    enemy.StopAllCoroutines();
                    return;
                }
                if (!enemy.enemyHurt)
                {
                    if (coroutine != null)
                    {
                        enemy.enemyHurt = false;
                        StopCoroutine(coroutine);
                    }
                    coroutine = enemy.Damaged();
                    enemy.Health -= 1;
                    StartCoroutine(coroutine);
                }

            }

        }
    }


}
