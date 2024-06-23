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

    [SerializeField] public int DeckLimit = 20;

    private List<Card> inventory;

    Deck deck;

    private string oldDeckName;

    [SerializeField] private TextMeshProUGUI _infographic;

    public static DeckBuilder instance;

    public void Awake(){
        
        instance = this;
        _infographic.text = "";
    }

    public bool AddCardToDeck(Card c){

        if(deck.contents.Count >= DeckLimit + 1){
            Debug.Log("Cannot Add Cards greater than: " + DeckLimit.ToString());
            return false;
        }

        deck.AddCard(c);
        

        if(c.type == CardType.Leader){
            _sWindow.ShowNonLeaders();
            _infographic.text = "";
            _dWindow.AddLeaderEntry(c);
        }else{
            _sWindow.RemoveCard(c);
            _dWindow.CreateAndAddCard(c);
            _dWindow.updateDeckLimitText(deck.contents.Count-1,DeckLimit);
        }

        return true;

       
    }

    public void RemoveCardFromDeck(Card c){
        deck.RemoveCard(c);


        if(c.type == CardType.Leader){
            PromptSelectLeader();
        }else{
            _sWindow.AddCard(c);
            _dWindow.updateDeckLimitText(deck.contents.Count-1,DeckLimit);
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
            _dWindow.SetTitleDisplay("");

        }else{ //else load deck from file and load into editor
            Debug.Log("Deck To Be Edited: " + deckToBeEdited);
            EditExistingDeck(playerData.GetDeck(deckToBeEdited));
            oldDeckName = deckToBeEdited;
            _dWindow.SetTitleDisplay(deckToBeEdited);
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
        }else if(deck.contents.Count < DeckLimit + 1){
            Debug.Log("Cannot Save a deck with less cards than: " +  DeckLimit.ToString());
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