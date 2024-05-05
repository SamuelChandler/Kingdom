using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : ScriptableObject
{
    public Sprite image;

    public new string name;

    //Cost 
    public int inspirationCost;

    public string description; 
}