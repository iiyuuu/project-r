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
    private float currentDistance;
    public GameObject firePoint;

    public float chaseRadius = 100f;
    public Vector3 homePosition;

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
        homePosition = transform.position;

        
        
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       // rb.rotation = angle;
        direction.Normalize();
        moveDirection = direction;
    }

    private void FixedUpdate()
    {

        CheckDistance();

    }
    void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius)
        {
            Debug.Log("Fuck you");
            transform.position = Vector2.MoveTowards(transform.position, target.position, (-1) * moveSpeed * Time.fixedDeltaTime);

        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius)
        {
            Debug.Log("Double fuck you");
            transform.position = Vector2.MoveTowards(transform.position, homePosition, (-1) * moveSpeed * Time.fixedDeltaTime);
        }
    }


}
