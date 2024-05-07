//this will hold a set of cards based on the time of release
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Unit",menuName = "Set/Create New Card Set")]
public class Set : ScriptableObject
{
    public List<Card> CardsInSet;

}