using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;
using System;

public class RangedAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 3f;
    public bool reloadTrigger = false;

    public static event Action OnPlayerFire;

    [SerializeField] private float fireRate;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool reloading = false;
    [SerializeField] private bool firing = false;
    [SerializeField] private float currentDelay;
    

    public Rigidbody2D body;
    public PlayerStats stats;
    public PlayerControls controls;
    public UnityEvent<float> OnReloading;


    IEnumerator coroutine;

    Vector2 mousePosition;

    private void Start()
    {
        OnReloading?.Invoke(currentDelay);

    }
    void Update()
    {
        //add crosshair
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(stats != null && controls != null && (stats.currentAmmo <= 0 || reloadTrigger) && stats.currentAmmo != stats.maxAmmo)
        {
            if (!reloading) 
            {
                currentDelay = reloadTime;
                coroutine = Refill();
                StartCoroutine(coroutine);
            }
            if (reloading)
            {
                currentDelay -= Time.deltaTime;
                OnReloading?.Invoke(currentDelay / reloadTime);
            }

        }
    }

    private void FixedUpdate()
    {
        if (gameObject.tag.Equals("Player"))
        {
            controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
            Vector2 aimDirection = mousePosition - body.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            body.rotation = aimAngle;
            if (controls.isDashing && coroutine != null)//reload cancelling
            {
                
                StopCoroutine(coroutine);
                OnReloading?.Invoke(0);
                reloading = false;
                reloadTrigger = false;
            }
        }
        
    }
    public void Fire()
    {
        if (!firing && !reloading)
        {
            OnPlayerFire?.Invoke();
            stats.currentAmmo--;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            Destroy(bullet, 1);
            StartCoroutine(FireRate());
        }
        
    }

    public IEnumerator Refill()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        stats.currentAmmo = stats.maxAmmo;
        reloading = false;
        reloadTrigger = false;
    }
    
    public IEnumerator FireRate()
    {
        firing = true;
        yield return new WaitForSeconds(1f / fireRate);
        firing = false;
    }
    


}
