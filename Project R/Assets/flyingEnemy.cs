using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingEnemy : MonoBehaviour
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
    public bool enemyHurt = false;
    public SpriteRenderer spriteRend;

    [Header("Attack Stuff")]
    public bool enemyMove = false;
    public float attackTime = 2f;
    RaycastHit2D hit;
    LayerMask layermask = 6;
    public Transform FirePoint;
    private bool canRotate = true;
    private bool lockedOn = false;
    private bool canDash = true;
    float dashingCooldown = 1.5f;
    private bool isDashing = false;
    public float dashingTime = 1f;
    private float startingDistance;
    public int Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                animator.SetTrigger("enemyDeath");
                rb = null;
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
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        spriteRend = GetComponent<SpriteRenderer>();
        homePosition = transform.position;
        startingDistance = Vector3.Distance(transform.position,target.position);
    }

    public void Update()
    {
        if (FirePoint.transform.rotation.eulerAngles.z > 179) { spriteRend.flipY = true; }
        else { spriteRend.flipY = false; }
    }

    private void FixedUpdate()
    {
        if (!enemyHurt)
        {
            Attack();

        }


    }

    void Death()
    {
        StopAllCoroutines();
        enemyHurt = false;
        Destroy(gameObject);
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


    public void Attack()
    {
        if (canRotate && canDash)
        {
            hit = Physics2D.Raycast(FirePoint.position, -FirePoint.right,layermask);
            if (hit.collider.CompareTag("Player"))
            {
                canRotate = false;
            }
            else
            {
                transform.Rotate(transform.forward);
            }
        }
        else
        {
            //Vector3 temp = new Vector3(0, -offset, 0);
            if (lockedOn && canDash && !isDashing)
            {
                if(Vector3.Distance(hit.collider.transform.position, transform.position) > 1.75f)
                {
                    rb.AddForce((hit.collider.transform.position - transform.position) * 1.25f, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce((hit.collider.transform.position - transform.position) * 3.25f, ForceMode2D.Impulse);
                }
  
      
                isDashing = true;
                //if it reaches a distance call cooldown
                //maybe do movetowards instead of add force
                StartCoroutine(DashCooldown());
                lockedOn = false;
                canRotate = true;


            }
            else
            {
                
                lockedOn = true;
            }

        }
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(dashingCooldown);//wait dash cd
        
        canDash = true;
    }


}
