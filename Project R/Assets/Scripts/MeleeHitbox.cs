using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{

    Vector2 rightAttackOffset;

    public Collider2D meleeCollider;

    public int attackDamage = 1;

    private void Start()
    {
        rightAttackOffset = transform.localPosition;
    }


    public void AttackRight()
    {
        meleeCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        meleeCollider.enabled = true;
        transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        meleeCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.Health -= attackDamage;
                StartCoroutine(enemy.Damaged());
            }
        }
    }













}
