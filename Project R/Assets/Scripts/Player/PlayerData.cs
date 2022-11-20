using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currency;
    public int attackDamage;
    public int maxHealth;
    public int maxAmmo;
    public bool savedata;

    public List<string> powerups;

    //spapwn in hub with current data on load
    public PlayerData (PlayerStats player)
    {
        List<ShopItems> powerupsList = player.smallPowerups;
        currency = player.currency;
        attackDamage = player.attackDamage;
        maxHealth = player.maxHealth;
        maxAmmo = player.maxAmmo;

        List<string> resources = new List<string>();
        foreach (ShopItems item in powerupsList)
        {
            resources.Add(AssetDatabase.GetAssetPath(item));
        }
        powerups = resources;
    }



}
