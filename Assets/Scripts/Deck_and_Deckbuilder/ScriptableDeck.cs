using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck",menuName = "Scriptable Deck")]
public class ScriptableDeck : ScriptableObject
{
    [SerializeField] public CardAndAmount[] deck;
    public string DeckName;
}

[Serializable]
public class CardAndAmount
{
    public Card card;
    public int amount;
}