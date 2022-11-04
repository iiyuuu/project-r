using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/Shop Item", order = 1)]
public class ShopItems : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;
}
