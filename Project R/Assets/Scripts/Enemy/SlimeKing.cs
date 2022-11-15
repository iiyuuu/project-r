using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlimeKing : Enemy
{
    int specialChange = 5;
    float specialAttackCooldownTime = 3f;

    bool canSpecial = true;
    bool shootRing = false;
    bool canDash = true;


    public GameObject firePoint0;
    public GameObject firePoint1;
    public GameObject firePoint2;
    public GameObject firePoint3;
    public GameObject firePoint4;
    public GameObject firePoint5;
    public GameObject firePoint6;
    public GameObject bulletPrefab;
    public float fireForce = 3f;

    Vector2 moveVector;
    public Vector2 targetPosition;
    public float dashForce;
    float dashCooldown = 3f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if(Health <= 15 && Health > 8)
        {
            spriteRend.color = Color.yellow;
            int rand = Random.Range(0,6);
            if(rand >= specialChange && canSpecial)
            {
                StartCoroutine(specialAttackCooldown());
            }
            else if (rand >= 0 && rand < specialChange && canDash)
            {
                StartCoroutine(dashCo());
            }
        }
        else if(Health <=8 && Health > 0)
        {
            spriteRend.color = Color.red;
            shootRing = true;
            int rand = Random.Range(0, 6);
            if (rand >= specialChange && canSpecial)
            {
                StartCoroutine(specialAttackCooldown());
            }
            else if(rand>=0 && rand < specialChange && canDash){
                StartCoroutine(dashCo());
            }
        }
    }

    private void specialAttack()
    {
        if (shootRing)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint1.transform.position, firePoint1.transform.rotation);
            bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint1.transform.up * fireForce, ForceMode2D.Impulse);
            Destroy(bullet, 5);
            bullet = Instantiate(bulletPrefab, firePoint2.transform.position, firePoint2.transform.rotation);
            bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint2.transform.up * fireForce, ForceMode2D.Impulse);
            Destroy(bullet, 5);
            bullet = Instantiate(bulletPrefab, firePoint3.transform.position, firePoint3.transform.rotation);
            bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint3.transform.up * fireForce, ForceMode2D.Impulse);
            Destroy(bullet, 5);
            bullet = Instantiate(bulletPrefab, firePoint4.transform.position, firePoint4.transform.rotation);
            bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint4.transform.up * fireForce, ForceMode2D.Impulse);
            Destroy(bullet, 5);
            bullet = Instantiate(bulletPrefab, firePoint5.transform.position, firePoint5.transform.rotation);
            bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint5.transform.up * fireForce, ForceMode2D.Impulse);
            Destroy(bullet, 5);
            bullet = Instantiate(bulletPrefab, firePoint6.transform.position, firePoint6.transform.rotation);
            bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint6.transform.up * fireForce, ForceMode2D.Impulse);
            Destroy(bullet, 5);
            bullet = Instantiate(bulletPrefab, firePoint0.transform.position, firePoint0.transform.rotation);
            bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint0.transform.up * fireForce, ForceMode2D.Impulse);
            Destroy(bullet, 5);
        }
        else
        {
            int rand = Random.Range(0, 7);
            switch (rand)
            {
                case 1:
                    GameObject bullet = Instantiate(bulletPrefab, firePoint1.transform.position, firePoint1.transform.rotation);
                    bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint1.transform.up * fireForce, ForceMode2D.Impulse);
                    Destroy(bullet, 5);
                    break;
                case 2:
                    bullet = Instantiate(bulletPrefab, firePoint2.transform.position, firePoint2.transform.rotation);
                    bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint2.transform.up * fireForce, ForceMode2D.Impulse);
                    Destroy(bullet, 5);
                    break;
                case 3:
                    bullet = Instantiate(bulletPrefab, firePoint3.transform.position, firePoint3.transform.rotation);
                    bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint3.transform.up * fireForce, ForceMode2D.Impulse);
                    Destroy(bullet, 5);
                    break;
                case 4:
                    bullet = Instantiate(bulletPrefab, firePoint4.transform.position, firePoint4.transform.rotation);
                    bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint4.transform.up * fireForce, ForceMode2D.Impulse);
                    Destroy(bullet, 5);
                    break;
                case 5:
                    bullet = Instantiate(bulletPrefab, firePoint5.transform.position, firePoint5.transform.rotation);
                    bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint5.transform.up * fireForce, ForceMode2D.Impulse);
                    Destroy(bullet, 5);
                    break;
                case 6:
                    bullet = Instantiate(bulletPrefab, firePoint6.transform.position, firePoint6.transform.rotation);
                    bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint6.transform.up * fireForce, ForceMode2D.Impulse);
                    Destroy(bullet, 5);
                    break;
                default:
                    bullet = Instantiate(bulletPrefab, firePoint0.transform.position, firePoint0.transform.rotation);
                    bullet.GetComponentInChildren<Rigidbody2D>().AddForce(firePoint0.transform.up * fireForce, ForceMode2D.Impulse);
                    Destroy(bullet, 5);
                    break;
            }
        }
    }

    public void Dash()
    {
        moveVector = ((Vector2)transform.position + pointerInput).normalized;
        targetPosition = moveVector * 1.25f + (Vector2)transform.position;//past the target 

        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, moveVector, Vector2.Distance((Vector2)transform.position, targetPosition), LayerMask.GetMask("Interactable"));
        if (hit.collider != null)
        {
            targetPosition = hit.transform.position;
        }
        rb.AddForce(moveVector * dashForce, ForceMode2D.Impulse);

    }
    IEnumerator specialAttackCooldown()
    {
        canSpecial = false;
        specialAttack();
        yield return new WaitForSeconds(specialAttackCooldownTime);
        canSpecial = true;
    }

    IEnumerator dashCo()
    {
        canDash = false;
        Dash();
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
