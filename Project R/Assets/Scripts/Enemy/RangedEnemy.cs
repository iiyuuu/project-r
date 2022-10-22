using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Components")]
    public Transform firePoint;
    public RangedAttack rangedAttack;
    public Vector2 hingeBase;
    public GameObject bulletPrefab;

    [Header("Stats")]
    public float aggroRange;
    public float fireForce;
    public float fireRate;

    [SerializeField]
    private bool isFiring = false;
    [SerializeField]
    private bool isRunning = false;

    // Start is called before the first frame update

    // Update is called once per frame
    public override void Start()
    {
        base.Start();
        hingeBase = rangedAttack.transform.localPosition;
    }

    protected override void FixedUpdate()
    {
        if (!enemyHurt)
        {
            if (spriteRend.flipX)//flip firing hinge
            {
                rangedAttack.transform.localPosition = new Vector2(-hingeBase.x, hingeBase.y);
            }
            else
            {
                rangedAttack.transform.localPosition = hingeBase;
            }
            rb.velocity = MoveInput.normalized * moveSpeed;//chase player, if at certain range, then fire
            CheckDistance();
            
        }
    }
    public override void CheckDistance()
    {
        if (Vector2.Distance(PointerInput, transform.position) <= chaseRadius)//if player is in aggro range
        {
            Debug.Log("Too Close");
            isRunning = true;
            transform.position = Vector2.MoveTowards(transform.position, PointerInput, (-1)*moveSpeed * Time.fixedDeltaTime);
            if(transform.position.x > PointerInput.x) { print("True");  spriteRend.flipX = true; }
            else { spriteRend.flipX = false; }
        }
        else if(Vector2.Distance(PointerInput, transform.position) > aggroRange && rb.velocity == Vector2.zero)
        {
            if (!isFiring)
            {
                Invoke("MoveBack", 3f);
            }
            
        }
        
    }

    public void Shoot()
    {
        //Debug.Log(PointerInput);
        if (!isRunning)
        {
            Vector2 difference = PointerInput - (Vector2)rangedAttack.transform.position;
            float aimAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
            rangedAttack.body.rotation = aimAngle;

            if (PointerInput.x > transform.position.x) { spriteRend.flipX = true; }//point at player
            else { spriteRend.flipX = false; }

            if (!isFiring)
            {
                animator.SetTrigger("attack");
                StartCoroutine(FiringCooldown());
            }
        }
        
    }

    public IEnumerator FiringCooldown()
    {
        
        isFiring = true;
        yield return new WaitForSeconds(1/ fireRate);
        isFiring = false;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        Destroy(bullet, 5);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
