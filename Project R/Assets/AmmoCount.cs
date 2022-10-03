using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCount : MonoBehaviour
{

    public PlayerStats playerStats;
    public TextMeshPro text;

    private void Update()
    {
        if(playerStats.projectilePowerUp > 0)
        {
            text.enabled = true;
            text.text = playerStats.projectilePowerUp.ToString();
        }
        else
        {
            text.enabled = false;
        }
        
    }
}
