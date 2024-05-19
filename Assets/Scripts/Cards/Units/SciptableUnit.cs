using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Unit",menuName = "Card/Scriptable Unit")]
public class ScriptableUnit : Card
{
    public Faction Faction;

    //player stats 
    public int health;
    public int attack;
    public int speed;

    //determines if the unit is a leader unit
    public bool _leader;
    
}

public enum Faction
{
    Hero = 0, 
    Enemy = 1,
    Neutral = 2
}
