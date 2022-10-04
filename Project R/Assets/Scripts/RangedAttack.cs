using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 3f;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool reloading = false;

    public Rigidbody2D body;
    public PlayerStats stats;


    IEnumerator coroutine;

    Vector2 mousePosition;

    void Update()
    {
        //add crosshair
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(stats.currentAmmo <= 0)
        {
            coroutine = Refill();
            if(coroutine != null) { StopCoroutine(coroutine); }
            if (!reloading) { StartCoroutine(coroutine); }
            
        }
    }

    private void FixedUpdate()
    {
        if (body.gameObject.tag.Equals("Player"))
        {
            Vector2 aimDirection = mousePosition - body.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            body.rotation = aimAngle;
        }
    }
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        Destroy(bullet, 5);
    }

    public IEnumerator Refill()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        stats.currentAmmo = stats.maxAmmo;
        reloading = false;
    }
}
