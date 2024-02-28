using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;

    //unit stats 
    public int Health;
    public int MaxHealth;
    public int Speed;
    public int attack;
    
}
