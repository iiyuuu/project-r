using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Rigidbody2D target;
    private float currDistance;
    private float detectionDistance = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currDistance = Vector3.Distance(target.position, firePoint.position);
        if(currDistance > detectionDistance)
        {
            Shoot();
        }
        
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }
}
