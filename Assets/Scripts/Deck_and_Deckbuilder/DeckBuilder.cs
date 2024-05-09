using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField]
    private CardSelectionWindow _sWindow;

    [SerializeField]
    private DeckView _dWindow;

    [SerializeField]
    private Set currentCardSet;

    Deck deck;

    public static DeckBuilder instance;

    public void Awake(){
        _sWindow.SetCards(currentCardSet);
        instance = this;
    }

    public void AddCardToDeck(Card c){
        _dWindow.AddCard(c);
    }

    public void RemoveCardFromDeck(Card c){
        _dWindow.RemoveCard(c);
    }




}