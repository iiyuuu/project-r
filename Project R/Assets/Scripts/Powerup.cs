using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Powerup : ScriptableObject
{
    public new string name;
    public string description;
    public int ID;

    public Sprite sprite;

    public int cost;
    
}
