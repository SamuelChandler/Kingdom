using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit",menuName = "Scriptable Structure")]
public class ScriptableStructure : ScriptableObject
{
    public Faction Faction;
    public Sprite image;

    public new string name;

    //Cost 
    public int inspirationCost;

    //size it will take up on the map
    public int height; 
    public int width; 

    //player stats 
    public int health;
    
}


