using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/Shop Item", order = 1)]
public class ShopItems : ScriptableObject
{
    public string title;
    public string description;
    public int ID;
    public bool enabled = false;
    public int baseCost;
    public bool purchasable = true;
    public Sprite sprite;

    public virtual void Activate(GameObject parent) { }
    
}
