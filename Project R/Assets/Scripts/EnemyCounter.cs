using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    GameObject[] Exit;
    public TextMeshProUGUI text;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Exit = GameObject.FindGameObjectsWithTag("Exit");
        foreach (GameObject door in Exit)
        {
            door.SetActive(true);
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
            foreach(GameObject door in Exit)
            {
                if(door != null)
                {
                    door.SetActive(false);
                }
                
            }
        }
    }
}
