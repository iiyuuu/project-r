using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Rigidbody2D body;
    public Weapon weapon;
    public PlayerStats stats;

    Vector2 mousePosition;

    void Update()
    {

         if (Input.GetMouseButtonDown(1))
         {
            if (stats.projectilePowerUp > 0)
            {
                weapon.Fire();
                stats.projectilePowerUp--;
            }
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
