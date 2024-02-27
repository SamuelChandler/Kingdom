using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Unit",menuName = "Scriptable Unit")]
public class SciptableUnit : ScriptableObject
{
    public Faction Faction;
    public BaseUnit UnitPrefab;

    
}

public enum Faction
{
    Hero = 0, 
    Enemy = 1
}
