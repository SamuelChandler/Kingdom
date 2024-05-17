using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
   public List<string> _decks;
   public List<int> _deckContents;

   public List<int> _cardInventory;

   public string PlayerName;


    public PlayerData(string name){
        PlayerName = name;
    }

    public void AddDeck(Deck d){
        foreach(Card c in d.contents){
            _deckContents.Add(DataPersistanceManager.instance.idTable.getID(c));
            _decks.Add(d.name);
        }
    }

    public void AddDeck(ScriptableDeck d){
        foreach(CardAndAmount c in d.deck){
            for(int i = 0; i < c.amount; i++){
                _deckContents.Add(DataPersistanceManager.instance.idTable.getID(c.card));
                _decks.Add(d.name);
            }
        }
    }

    public void AddDeckToInventory(ScriptableDeck d){
        foreach(CardAndAmount c in d.deck){
            _cardInventory.Add(DataPersistanceManager.instance.idTable.getID(c.card)); 
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
        Deck outputDeck = new Deck(n);

        outputDeck.name = n;
        outputDeck.contents = new List<Card>();

        //Add cards that align with decks to the deck data object
        for(int i =0; i < _decks.Count;i++){
            if(_decks[i] == n){
                outputDeck.contents.Add(DataPersistanceManager.instance.idTable.getCard(_deckContents[i]));
            }
        }

        return outputDeck;
    }
    
    public List<Deck> GetAllDecks(){
        List<Deck> res = new List<Deck>();
        Dictionary<string,List<Card>> decks = new Dictionary<string, List<Card>>();
        int i = 0;
        Debug.Log(_deckContents.Count);
        Debug.Log(_decks.Count);

        while(i < _decks.Count){
            
            Card c = DataPersistanceManager.instance.idTable.getCard(_deckContents[i]);
            string name = _decks[i];

            if(decks.ContainsKey(name)){
                decks[name].Add(c);
            }else{
                decks.Add(name,new List<Card>());
                decks[name].Add(c);
            }

            i++;
        }

        foreach(string dName in decks.Keys){
            
            Deck tempDeck = new Deck(dName);

            foreach(Card card in decks[dName]){

                tempDeck.AddCard(card);

                if(card.type == CardType.Leader){
                    tempDeck._leader = card;
                }
            }

            res.Add(tempDeck);
        }

        return res;
    }
}
