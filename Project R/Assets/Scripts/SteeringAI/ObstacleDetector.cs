using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetector : Detector
{
    [SerializeField]
    private float detectionRadius;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private bool showGizmos = true;

    Collider2D[] colliders;

    public override void Detect(AIData aiData)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);
        aiData.obstacles = colliders;

        foreach(Collider2D collider in colliders)
        {
            SpriteRenderer render = collider.GetComponent<SpriteRenderer>();
            if (render != null)
            {
                if (gameObject.transform.position.y > collider.transform.position.y)
                {
                    render.sortingOrder = gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder + 1;
                }
                else
                {
                    render.sortingOrder = gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder - 1;
                }
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos == false)
            return;
        if (colliders != null)
        {
            Gizmos.color = Color.red;
            foreach (Collider2D obstacleCollider in colliders)
            {
                Gizmos.DrawSphere(obstacleCollider.transform.position, 0.05f);
            }
        }
    }
}