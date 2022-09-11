using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 3;
    private int currentHealth;
    [SerializeField] float moveSpeed = 25f;
    private Rigidbody2D rb;
    public Transform target;
    private Vector2 moveDirection;
    private GameObject[] enemies;
   

  

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        moveDirection = direction;
    }
    private void FixedUpdate()
    {
        moveEnemy();
    }

    void moveEnemy()
    {
      rb.MovePosition((Vector2)transform.position + (moveDirection * moveSpeed * Time.deltaTime));
       
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    
}
