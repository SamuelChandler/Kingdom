using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] public CardIDTable idTable;
    [SerializeField] public MapIDTable mapIDTable;
    [SerializeField] public StoryTracker _sTracker;

    private PlayerData playerData;

    private List<IDataPersistance> dataPersistanceObjects;
    public ScriptableDeck _starterDeck;
    private FileDataHandler dataHandler; 




    public static DataPersistanceManager instance {get; private set; }

    private void Awake(){

        if(instance != null){
            Debug.Log("Too Many Data Managers");
        }

        instance = this;
    }

    public void NewGame(string playerName){
        
        foreach(StoryEvent s in _sTracker.events){
            s.completed = false;
        }

        playerData = new PlayerData(playerName);
        playerData._deckContents = new List<int>();
        playerData._decks = new List<string>();
        playerData._cardInventory = new List<int>();
        playerData._storyEventsCompleted = new List<int>();


        this.playerData.AddDeck(_starterDeck);
        this.playerData.AddDeckToInventory(_starterDeck);
        this.playerData.SelectedDeck = _starterDeck.name;
        playerData.MapLocation = new Vector2(0f,0f);
    }

    public void LoadGame(){
        //TD Load any Saved Data from a file from the data handler
        this.playerData = dataHandler.Load();

        //if there is no data then start a new game
        if(this.playerData == null){
            Debug.Log("No Data Found");
            NewGame("NewPlayer");
        }

        _sTracker.SetEventState(playerData._storyEventsCompleted);

        foreach(IDataPersistance dpObject in dataPersistanceObjects){
            dpObject.LoadData(playerData);
        }

    }

    public void SaveGame(){

        foreach(int i in _sTracker.GetEventState()){
            playerData.SetEventCompleted(i);
        }

        //TD pass data to other scripts so they can update it
        foreach(IDataPersistance dpObject in dataPersistanceObjects){
            dpObject.SaveData(ref playerData);
        }

        //TD save that data to a file using the data handler
        dataHandler.Save(playerData);
    }

    public void SuperGame(string playerName){
        foreach(StoryEvent s in _sTracker.events){
            s.completed = false;
        }

        playerData = new PlayerData(playerName);
        playerData._deckContents = new List<int>();
        playerData._decks = new List<string>();
        playerData._cardInventory = new List<int>();
        playerData._storyEventsCompleted = new List<int>();


        this.playerData.AddDeck(_starterDeck);
        this.playerData.AddDeckToInventory(_starterDeck);

        //add all cards to inventory
        foreach(Card card in idTable.getAllCards()){
            this.playerData.AddCardToInventory(card);
        }

        this.playerData.SelectedDeck = _starterDeck.name;
        playerData.MapLocation = new Vector2(0f,0f);
    }

    private void Start(){
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }

    private void OnApplicationQuit(){
        SaveGame();
    }

}
