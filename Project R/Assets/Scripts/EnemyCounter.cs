using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    GameObject[] Teleporters;
    public TextMeshProUGUI text;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
        foreach (GameObject teleporter in Teleporters)
        {
            teleporter.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        EnemyCount();
    }

    public void EnemyCount()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        text.text = "Enemies: " + enemies.Length;
        if (enemies.Length <= 0)
        {
            foreach(GameObject teleporter in Teleporters)
        {
                teleporter.SetActive(true);
            }
        }
    }
}
