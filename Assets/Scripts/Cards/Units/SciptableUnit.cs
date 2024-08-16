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

    public bool _swift;

    public Effect OnDeath;

    public Effect OnPlay;

    public Effect OnAttack;

    public Effect OnEndTurn; 

    public Effect OnStartOfTurn;

    public Effect beforeMoving;

    public Effect afterMoving;

    public Effect OnAllyDeath;

    public RuntimeAnimatorController animatorController;

    
}

public enum Faction
{
    Hero = 0, 
    Enemy = 1,
    Neutral = 2
}
