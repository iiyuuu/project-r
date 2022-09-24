using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public enum ItemType
    {
        Health,
        Melee,
        Ranged,
        Healing,
        Speed,
        AttackRange,
        Armor,
        Special
    }

    public enum Tier
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary

    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Health:        return 1;
            case ItemType.Melee:         return 2;
            case ItemType.Ranged:        return 3;
            case ItemType.Healing:       return 4;
            case ItemType.Speed:         return 5;
            case ItemType.AttackRange:   return 6;
            case ItemType.Armor:         return 7;
            case ItemType.Special:       return 8;
            
        }
    }

    /*public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Health:       return GameAssets.i.s_Health;
            case ItemType.Melee:        return GameAssets.i.s_Melee;
            case ItemType.Ranged:       return GameAssets.i_s_Ranged;
            case ItemType.Healing:      return GameAssets.i_s_Healing;
            case ItemType.Speed:        return GameAssets.i_s_Speed;
            case ItemType.AttackRange:  return GameAssets.i_s_AttackRange;
            case ItemType.Armor:        return GameAssets.i_s_Armor;
            case ItemType.Special:      return GameAssets.i_s_Special;


        }
        
    }*/
}
