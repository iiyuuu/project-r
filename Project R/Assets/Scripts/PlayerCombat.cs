using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 1;

    public PlayerStats stats;


    // Update is called once per frame
    void Update()
    {

    }

    void OnMelee()
    {
        ////attack animation
        ////detect enemies in range
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        ////Damage enemies
        //foreach(Collider2D enemy in hitEnemies)
        //{
        //    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        //}

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.tag == "Enemy")
        //{
        //    Enemy enemey = other.GetComponent<Enemy>();
        //    if(enemy != null)
        //    {
        //        stats.DamageTaken(1);
        //    }
        //}
        return;
    }

}
