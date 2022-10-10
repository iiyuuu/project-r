using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 3;
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    public Transform target;

    public float chaseRadius;
    public Vector3 homePosition;
    public bool ranged;


    public Animator animator;

    [Header("Damage Indicator")]
    public float iFrameDuration;
    [SerializeField] int numberOfFlashes;
    [SerializeField] public bool enemyHurt = false;
    public SpriteRenderer spriteRend;


    public int Health
    {
        set
        {
            health = value;
            if(health <= 0)
            {
                animator.SetTrigger("enemyDeath");
                rb = null;
            }
        }
        get
        {
            return health;
        }
    }
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        spriteRend = GetComponent<SpriteRenderer>();
        homePosition = transform.position;
    }

    public void Update()
    {
        if (target.position.x > transform.position.x) { spriteRend.flipX = true; }
        else { spriteRend.flipX = false; }
    }
    private void FixedUpdate()
    {
        if (!enemyHurt)
        {
            if (!ranged) { CheckDistance(); }
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
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                
        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius)
        {
            Invoke("MoveBack", 1f);//1s delay before moving back
        }
        
    }

    public void MoveBack()
    {
        if(Vector2.Distance(target.position, transform.position) > chaseRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, homePosition, moveSpeed * Time.fixedDeltaTime);
        }
        
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag.Equals("Enemy"))
        {
            if (enemyHurt)//checks if enemy is hurt and hurts both objects
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.rb.velocity = Vector2.zero;
                rb.velocity = Vector2.zero;
                print("collision");
                if (enemy != null)
                {
                    if (!enemy.enemyHurt)
                    {
                        enemy.Health -= 1;
                        StartCoroutine(enemy.Damaged());
                    }

                }

            }
            
        }
        
    }


}
