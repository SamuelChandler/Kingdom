using System.Collections.Generic;
using UnityEngine;

public class Deck{
    public string name;
    public List<Card> contents;

    public void AddCard(Card card){
        contents.Add(card);
    }

    public void RemoveCard(Card card){
        contents.Remove(card);
    }
}

