using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour,IDataPersistance
{
    [SerializeField]
    private CardSelectionWindow _sWindow;

    [SerializeField]
    public DeckView _dWindow;

    private List<Card> inventory;

    Deck deck;

    private string oldDeckName;

    [SerializeField] private TextMeshProUGUI _infographic;

    public static DeckBuilder instance;

    public void Awake(){
        
        instance = this;
        _infographic.text = "";
    }

    public void AddCardToDeck(Card c){
        deck.AddCard(c);

        if(c.type == CardType.Leader){
            _sWindow.ShowNonLeaders();
            _infographic.text = "";
            _dWindow.AddLeaderEntry(c);
        }else{
             _dWindow.CreateAndAddCard(c);
        }

       
    }

    public void RemoveCardFromDeck(Card c){
        deck.RemoveCard(c);

        if(c.type == CardType.Leader){
            PromptSelectLeader();
        }
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
        PromptSelectLeader();

    }

    public void EditExistingDeck(Deck d){

        _sWindow.SetCards(inventory);

        foreach(Card argCard in d.contents){

            AddCardToDeck(argCard);
        }

        if(deck._leader == null){
            PromptSelectLeader();
        }else{
            _sWindow.ShowNonLeaders();
            _infographic.text = "";
        }
    }

    public void SaveData(ref PlayerData playerData)
    {   
        if(deck._leader == null){
            Debug.Log("Cannot Save A Deck Without A leader");
            _infographic.text = "Cannot Save A Deck Without A Leader";
            return;
        }

        playerData.RemoveDeck(oldDeckName);
        oldDeckName = deck.name;
        playerData.AddDeck(deck);
    }

    public void SetTitle(string title){
        deck.name = title;
    }

    public void PromptSelectLeader(){
        _sWindow.ShowOnlyLeaders();
        _infographic.text = "Please Select A Leader Unit for the Deck";
    }
}