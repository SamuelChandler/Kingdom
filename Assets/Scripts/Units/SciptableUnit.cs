using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Unit",menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject
{
    public Faction Faction;
    public Sprite image;

    public new string name;

    //Cost 
    public int inspirationCost;

    //player stats 
    public int health;
    public int MaxHealth;
    public int attack;
    public int speed;
    
}

public enum Faction
{
    Hero = 0, 
    Enemy = 1
}
