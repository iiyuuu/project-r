using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 25f;
    public int health = 3;
    private Rigidbody2D rb;
    public Transform target;
    private Vector2 moveDirection;

    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    [SerializeField] public CollisionHandler collision_h;

    Animator animator;

    [Header("Damage Indicator")]
    public float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] public bool enemyHurt = false;
    private SpriteRenderer spriteRend;

    public int Health
    {
        set
        {
            health = value;

            if(health <= 0)
            {
                Destroy(gameObject);
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
        homePosition = this.transform;
        //Debug.Log(homePosition.position);
    }

    private void Update()
    {
        
        if (enemyHurt) { return; }


        //Vector3 direction = target.position - transform.position;
        ////float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        ////rb.rotation = angle;
        //direction.Normalize();
        //moveDirection = direction;
    }
    private void FixedUpdate()
    {
        moveEnemy(moveDirection);
        CheckDistance();
    }

    void moveEnemy(Vector2 direction)
    {
        //rb.MovePosition((Vector2)transform.position + (moveDirection * moveSpeed * Time.fixedDeltaTime));
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
    
    void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                
        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.fixedDeltaTime);
        }
        
    }

}
