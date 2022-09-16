using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D rb;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerStats player = hitInfo.GetComponent<PlayerStats>();
        if(player != null)
        {
            player.DamageTaken(damage);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
