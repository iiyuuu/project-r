using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeController : MonoBehaviour
{

    Vector2 rightAttackOffset;

    public CollisionHandler collisionHandler;

    [Header("Attack Properties")]
    public int attackDamage = 1;
    public float attackRate = 2f;
    public float attackRange = 0.5f;

    [Header("Backend stuff")]
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    IEnumerator coroutine;
    IEnumerator coroutine2;

    public bool flip;


    private void Start()
    {
        rightAttackOffset = transform.localPosition;
        flip = false;
    }

    public void Attack()
    {
        if (flip)
        {
            transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
        }
        else
        {
            transform.localPosition = rightAttackOffset;
        }
        animator.SetTrigger("isAttacking");
        List<Collider2D> enemiesAbove = new List<Collider2D>(Physics2D.OverlapAreaAll(new Vector2(attackPoint.position.x - .25f, attackPoint.position.y + .1f), new Vector2(transform.localPosition.x - .2f, transform.localPosition.y + 0f), enemyLayers));
        List<Collider2D> hitEnemies = new List<Collider2D>(Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers));
        List<Collider2D> allHits = new List<Collider2D>(Physics2D.OverlapCircleAll(attackPoint.position, attackRange));
        foreach (Collider2D hit in enemiesAbove)
        {
            hitEnemies.Add(hit);
        }
        foreach (Collider2D hit in allHits)
        {
            if (hit.CompareTag("Enemy Projectile"))
            {
                Destroy(hit.gameObject);
            }
        }
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null && !enemyComponent.enemyHurt)
            {
                Rigidbody2D enemyBody = enemyComponent.GetComponent<Rigidbody2D>();
                Vector2 difference = enemyBody.transform.position - attackPoint.position;
                difference = difference.normalized * collisionHandler.thrust;

                if (coroutine != null) { StopCoroutine(coroutine); }
                coroutine = collisionHandler.kbCoroutine(enemyBody);
                enemyBody.velocity = Vector2.zero;
                enemyBody.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(collisionHandler.kbCoroutine(enemyBody));

                if (coroutine2 != null) { StopCoroutine(coroutine); }
                coroutine2 = enemyComponent.Damaged();
                    
                   
                enemyComponent.Health -= attackDamage;
                StartCoroutine(coroutine2);

            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null) { return; }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }













}
