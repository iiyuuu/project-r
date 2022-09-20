using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 shootDir;
    public float moveSpeed = 2f;

    public void Setup(Vector3 shootDir)
    {
<<<<<<< Updated upstream
        this.shootDir = shootDir;
=======
        rb.MovePosition(transform.right * speed * Time.deltaTime);
>>>>>>> Stashed changes
    }

    private void Update()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }
}
