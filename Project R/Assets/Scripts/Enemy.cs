using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 3;
    [SerializeField] float moveSpeed = 25f;
    private Rigidbody2D rb;
    public Transform target;
    private Vector2 moveDirection;
    public float detectionDistance = 10f;
    private float currentDistance;

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
    

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        currentDistance = Vector3.Distance(target.position,transform.position);
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        direction.Normalize();
        moveDirection = direction;
    }
    private void FixedUpdate()
    {
        if(currentDistance < detectionDistance)
        {
            moveEnemy(moveDirection);
        }
        
    }

    void moveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (moveDirection * moveSpeed * Time.fixedDeltaTime));
    }


    
}
