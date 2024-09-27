using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit",menuName = "Card/Structure")]
public class ScriptableStructure : Card
{
    public Faction Faction;

    //size it will take up on the map
    public int height; 
    public int width; 

    //player stats 
    public int health;

    public Effect EndOfTurn;

    public Effect StartOfTurn;

    public Effect WhenDestroyed;

    public Effect OnSummon;

    public Effect OnAllyDeath;

    public int allyAttackBoost;
    
}


