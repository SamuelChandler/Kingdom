using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour,IDataPersistance
{
    [SerializeField]
    private CardSelectionWindow _sWindow;

    [SerializeField]
    private DeckView _dWindow;

    private List<Card> inventory;

    Deck deck;

    private string oldDeckName;

    public static DeckBuilder instance;

    public void Awake(){
        
        instance = this;
    }

    public void AddCardToDeck(Card c){
        deck.AddCard(c);
        _dWindow.CreateAndAddCard(c);
    }

    public void RemoveCardFromDeck(Card c){
        _dWindow.RemoveCard(c);
    }

    public void LoadData(PlayerData playerData)
    {

        string deckToBeEdited = PlayerPrefs.GetString("DeckToBeEdited");

        
        //clear previous data
        deck = new Deck(deckToBeEdited);
        inventory = playerData.GetInventory();

        

        //if deck is new do not populate the deck
        if(deckToBeEdited == null){
            CreateNewDeck();
        }else{ //else load deck from file and load into editor
            Debug.Log("Deck To Be Edited: " + deckToBeEdited);
            EditExistingDeck(playerData.GetDeck(deckToBeEdited));
            oldDeckName = deckToBeEdited;
        }

        PlayerPrefs.SetString("DeckToBeEdited",null);
    }

    public void CreateNewDeck(){

        _sWindow.SetCards(inventory);

    }

    public void EditExistingDeck(Deck d){

        _sWindow.SetCards(inventory);

        foreach(Card argCard in d.contents){

            AddCardToDeck(argCard);
        }

    }

    public void SaveData(ref PlayerData playerData)
    {   
        playerData.RemoveDeck(oldDeckName);
        playerData.AddDeck(deck);
    }

    public void SetTitle(string title){
        deck.name = title;
    }
}