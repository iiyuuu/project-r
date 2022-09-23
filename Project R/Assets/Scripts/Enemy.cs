using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 3;
    [SerializeField] float moveSpeed = 3f;
    private Rigidbody2D rb;
    public Transform target;

    public float chaseRadius;
    public Vector3 homePosition;

    Animator animator;

    [Header("Damage Indicator")]
    public float iFrameDuration;
    [SerializeField] int numberOfFlashes;
    [SerializeField] public bool enemyHurt = false;
    private SpriteRenderer spriteRend;

    public int Health
    {
        set
        {
            health = value;
            if(health <= 0)
            {
                animator.SetTrigger("slimeDeath");
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
        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        spriteRend = GetComponent<SpriteRenderer>();
        homePosition = transform.position;
    }

    private void Update()
    {
        
        //Vector3 direction = target.position - transform.position;
        ////float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        ////rb.rotation = angle;
        //direction.Normalize();
        //moveDirection = direction;
    }
    private void FixedUpdate()
    {
        if (enemyHurt) { return;}
        CheckDistance();
    }

    public IEnumerator Damaged()
    {
        if(rb != null)
        {
            for(int i = 0; i < numberOfFlashes; i++)
            {
                spriteRend.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFrameDuration / numberOfFlashes);
                spriteRend.color = Color.white;
                
            }
            enemyHurt = false;
        }
        
        
    }
    
    void CheckDistance()
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

    void MoveBack()
    {
        if(Vector2.Distance(target.position, transform.position) > chaseRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, homePosition, moveSpeed * Time.fixedDeltaTime);
        }
        
    }

    void Death()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            other.rigidbody.velocity = Vector2.zero;
            if (enemyHurt)//checks if enemy is hurt and hurts both objects
            {
                if(!enemy.enemyHurt)
                {
                    enemy.Health -= 1;
                    enemy.enemyHurt = true;
                    StartCoroutine(enemy.Damaged());
                }
                
            }
        }
        
    }


}
