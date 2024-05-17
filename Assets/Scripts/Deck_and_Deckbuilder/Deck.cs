using System.Collections.Generic;
using UnityEngine;

public class Deck{
    public string name;
    public List<Card> contents;

    public Card _leader;

    public Deck(string n){
        name = n;
        contents = new List<Card>();
    }

    public void AddCard(Card card){
        contents.Add(card);
    }

    public void RemoveCard(Card card){
        contents.Remove(card);
    }
}

