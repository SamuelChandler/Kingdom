using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
   public List<string> _decks;
   public List<Card> _deckContents;

   public List<Card> _cardInventory;

   public string PlayerName;


    public PlayerData(string name){
        PlayerName = name;
    }

    public void AddDeck(Deck d){
        foreach(Card c in d.contents){
            _deckContents.Add(c);
            _decks.Add(d.name);
        }
    }

    public void AddDeck(ScriptableDeck d){
        foreach(CardAndAmount c in d.deck){
            for(int i = 0; i < c.amount; i++){
                _deckContents.Add(c.card);
                _decks.Add(d.name);
            }
        }
    }

    public void AddDeckToInventory(ScriptableDeck d){
        foreach(CardAndAmount c in d.deck){
            _cardInventory.Add(c.card); 
        }
    }

    public void RemoveDeck(string n){
        for(int i =0; i < _decks.Count;i++){
            if(_decks[i] == n){
                _decks.RemoveAt(i);
                _deckContents.RemoveAt(i);
            }
        }
    }

    public Deck GetDeck(string n){
        Deck outputDeck = new Deck();

        outputDeck.name = n;
        outputDeck.contents = new List<Card>();

        //Add cards that align with decks to the deck data object
        for(int i =0; i < _decks.Count;i++){
            if(_decks[i] == n){
                outputDeck.contents.Add(_deckContents[i]);
            }
        }

        return outputDeck;
    }
    

}
