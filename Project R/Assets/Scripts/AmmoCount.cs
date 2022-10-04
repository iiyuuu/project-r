using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCount : MonoBehaviour
{

    public PlayerStats playerStats;
    public TextMeshPro text;

    void Update()
    {
        text.enabled = true;
        text.text = playerStats.currentAmmo.ToString();
    }
}
