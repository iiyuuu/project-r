using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/Melee Boost", order = 3)]
public class ShopMeleeBoost : ShopItems
{
    public int amount;
    public int level = 1;
    private int maxLevel = 3;

    public override void Activate(GameObject parent)
    {
        parent.GetComponent<PlayerStats>().smallPowerups[ID].enabled = true;
        parent.GetComponent<PlayerStats>().attackDamage += amount;
        LevelUp();
    }

    public void LevelUp()
    {
        if (level < maxLevel)
        {
            level += 1;
            baseCost *= 10 / level;
            amount += 1;
        }
        else
        {
            purchasable = false;
        }
    }
}
