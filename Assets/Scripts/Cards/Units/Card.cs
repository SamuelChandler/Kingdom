using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Card : ScriptableObject
{   
    public Sprite image;

    public new string name;

    //Cost 
    public int inspirationCost;

    public string description; 
    public CardType type;
  
}

[Serializable]
public enum CardType{
    Leader,
    Unit,
    Spell,
    Structure
}