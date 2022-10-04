using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject firePoint;
    public float attackRadius;

    // Start is called before the first frame update

    // Update is called once per frame

    public new void Update()
    {
    }
    private void FixedUpdate()
    {
        CheckDistance();
    }
    public override void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius)//if player is in aggro range
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, (-1)*moveSpeed * Time.fixedDeltaTime);
            if(transform.position.x > target.position.x) { spriteRend.flipX = true; }
            else { spriteRend.flipX = false;}
        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius && Vector2.Distance(target.position, transform.position) < attackRadius)
        {
            //Debug.Log("Fire");
            if (target.position.x > transform.position.x) { spriteRend.flipX = true; }
            else { spriteRend.flipX = false; }
        }
        else
        {
            Invoke("MoveBack", 1f);
        }

    }


}
