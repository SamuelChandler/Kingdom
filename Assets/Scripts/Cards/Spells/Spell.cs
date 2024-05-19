using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Spell : Card
{

    [SerializeField] public Faction faction;
    
    [SerializeField] public Effect[] effects;

    [SerializeField] public int target;
}
