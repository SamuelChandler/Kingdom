using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Deck{
    public string name;
    public List<Card> contents;

    public static int MAX_NUMBER_OF_COPYS = 3;

    public Dictionary<Card,int> numberOfEachCard;

    public Card _leader;

    public Deck(string n){
        name = n;
        _leader = null;
        contents = new List<Card>();
        numberOfEachCard = new Dictionary<Card, int>();
    }

    public void AddCard(Card card){
        if(numberOfEachCard.ContainsKey(card)){
            //deck contains at least one of the card

            if(numberOfEachCard[card] < MAX_NUMBER_OF_COPYS){
                //if the current card is not at its maximum ammount
                numberOfEachCard[card]++;
                contents.Add(card);
            }else{
                Debug.Log("Cannot Add any more of this card");
            }

        }else if(card.type != CardType.Leader){
            //card is not a leader unit
            numberOfEachCard.Add(card,1);
            contents.Add(card);

        }else{
            if(_leader == null){
                //if there is not a leader unit
                _leader = card;
                contents.Add(card);
                numberOfEachCard.Add(card,1);
            } else{
                Debug.Log("Leader Unit already exists");
            }
        }
        
    }

    public void RemoveCard(Card card){
        contents.Remove(card);
        numberOfEachCard[card]--;

        if(numberOfEachCard[card] <= 0){
            numberOfEachCard.Remove(card);
        }

        if(card.type == CardType.Leader){
            Debug.Log("Leader Removed");
            _leader = null;
        }
    }

    public void ShuffleDeck(){
        int n = contents.Count;
        

        while(n > 1){
            n--;
            int k = Random.Range(0,n);
            Card temp = contents[n];
            contents[n] = contents[k];
            contents[k] = temp;
        }

        Debug.Log("Shuffled Deck");
    }

    public Card DrawCard(){

        if(contents.Count == 0){
            Debug.Log("Deck is empty");
            return null;
        }

        Card temp = contents[0];
        contents.RemoveAt(0);

        return temp;
        
    }
}

