using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currency;
    public int attackDamage;
    public int maxHealth;
    public int maxAmmo;
    public bool savedata;
    public List<ShopItems> smallPowerups;

    //spapwn in hub with current data on load
    public PlayerData (PlayerStats player)
    {
        currency = player.currency;
        attackDamage = player.attackDamage;
        maxHealth = player.maxHealth;
        maxAmmo = player.maxAmmo;
        smallPowerups = player.smallPowerups;
    }



}
