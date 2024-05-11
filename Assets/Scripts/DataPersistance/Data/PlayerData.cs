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

    public void RemoveDeck(string n){
        for(int i =0; i < _decks.Count;i++){
            if(_decks[i] == n){
                _decks.RemoveAt(i);
                _deckContents.RemoveAt(i);
            }
        }
    }

    public Deck GetDeck(string n){
        Deck ret = new Deck();

        ret.name = n;

        for(int i =0; i < _decks.Count;i++){
            if(_decks[i] == n){
                
                ret.AddCard(_deckContents[i]);
            }
        }
        
        return ret;

    }

}
