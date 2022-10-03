using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Rigidbody2D body;
    public Weapon weapon;
    public PlayerStats stats;

    public SpriteRenderer spriteRenderer;
    Vector2 mousePosition;

    private void Start()
    {
        spriteRenderer.enabled = false;
    }
    void Update()
    {

         if (stats.projectilePowerUp > 0)
         {
            spriteRenderer.enabled = true;
            if (Input.GetMouseButtonDown(1))//change this to new input system
            {
                weapon.Fire();
                stats.projectilePowerUp--;
            }
         }
        else
        {
            spriteRenderer.enabled = false;
        }


        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 aimDirection = mousePosition - body.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        body.rotation = aimAngle;
    }
}
