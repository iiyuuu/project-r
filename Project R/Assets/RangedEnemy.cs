using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public int health = 3;
    [SerializeField] float moveSpeed = 3f;
    private Rigidbody2D rb;
    public Transform target;
    private Vector2 moveDirection;
    public float detectionDistance = 1f;
    [SerializeField]private float currentDistance;

    public int Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        get
        {
            return health;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        currentDistance = Vector3.Distance(target.position, transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        moveDirection = direction;
    }

    private void FixedUpdate()
    {
        if (currentDistance < detectionDistance)
        {
            Debug.Log("Detected");
            moveEnemy(moveDirection);
        }

    }

    void moveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position - (direction * moveSpeed * Time.fixedDeltaTime));
    }
}
