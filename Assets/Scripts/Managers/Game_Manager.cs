using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using Unity.VisualScripting;
using System;
using UnityEditor;

public class Game_Manager : MonoBehaviour,IDataPersistance
{
    public static Game_Manager instance;
    public GameState GameState;
    private int Level;

    [SerializeField] public static float MoveDuration = 0.5f;

    public EnemyAI eAI;

    public int CurrentInspiration;
    [SerializeField] private int CurrentMaxInspiration; 
    private int MaxInspiration = 10;

    //feilds relating to drawing cards and hand size
    [SerializeField] private int cardsDrawnPerTurn = 1;
    [SerializeField] private int StartingHandSize = 3;

    private bool GameWin;

    private Deck deck;

    private void Awake()
    {
        instance = this;
        GameWin = false;
        Level = PlayerPrefs.GetInt("Level Selected");
        CurrentInspiration = CurrentMaxInspiration;

    }

    private void Start()
    {
        //start of game menu manager updates with game info
        Menu_Manager.instance.SetMessenger("Level: "+ Level);
        Menu_Manager.instance.UpdateIBar(CurrentInspiration, CurrentMaxInspiration, MaxInspiration);

        ChangeState(GameState.GenerateGrid);
    }

    //used to change the state of the game 
    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                Board_Manager.instance.generateGrid();
                Board_Manager.instance.SpawnMapStructures();
                break;
            case GameState.SpawnHero:
                Board_Manager.instance.SpawnLeader((ScriptableUnit)deck._leader);
                PlayerPregameActions();
                ChangeState(GameState.SpawnEnemies);
                break;
            case GameState.SpawnEnemies:
                Board_Manager.instance.SpawnBoss();
                Unit_Manager.instance.SpawnEnemies();
                break;
            case GameState.HeroesTurn:
                StartPlayerTurn();
                break;
            case GameState.EnemiesTurn:
                eAI.StartTurn();
                break;
            case GameState.GameWin:
                ResolveGameWin();
                break;
            case GameState.GameLoss:
                Menu_Manager.instance.showLossScreen();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
    }
    
    //used to increase the Inspiration limit of the player by 1
    public void IncreaseInspirationLimit()
    {
        
        CurrentMaxInspiration += 1;
        
        if (CurrentMaxInspiration > MaxInspiration)
        {
            CurrentMaxInspiration -= 1;
        }

        Menu_Manager.instance.UpdateIBar(CurrentInspiration, CurrentMaxInspiration, MaxInspiration);
    }

    //used to decrease the Inspiration of the player
    public void DecreaseCurrentInsperation(int i)
    {
        CurrentInspiration -= i;

        Menu_Manager.instance.UpdateIBar(CurrentInspiration, CurrentMaxInspiration, MaxInspiration);
    }

    //returns whether a unit can be played
    public bool CanBePlayed(BaseUnit unit,Tile dest)
    {
        //cannot be played if cost is greater than  current inspiration
        if(unit.unit.inspirationCost > CurrentInspiration)
        {
            Menu_Manager.instance.SetMessenger("Not enough inspiration to play this card");
            return false;
        }

        foreach (BaseHero a in Board_Manager.instance._heroes){
            if(Board_Manager.instance.WithinOne(a,dest)){
                return true;
            }
        }
        Menu_Manager.instance.SetMessenger("cannot summon here");
        return false;
    }

    public bool CanBePlayed(Spell s, Tile t){

        if(s.inspirationCost > CurrentInspiration){
                Menu_Manager.instance.SetMessenger("Not enough inspiration to play this spell");
                return false;
        }

        if(t != null){
            //if the tile is null then the spell does not target one tile 
            if(t.OccupiedStructure == null && t.OccupiedUnit == null){
                Menu_Manager.instance.SetMessenger("there is not target in this location");
                return false;   
            }

            if(t.OccupiedStructure != null){
                if(t.OccupiedStructure._structure.Faction != s.TargetFaction){
                    Menu_Manager.instance.SetMessenger("Cannot target this unit with this spell");
                    return false;
                }
            }

             if(t.OccupiedUnit != null){
                if(t.OccupiedUnit.unit.Faction != s.TargetFaction){
                    Menu_Manager.instance.SetMessenger("Cannot target this unit with this spell");
                    return false;
                }
            }
        }
        
        return true;
    }

    public bool CanBePlayed(AllyStructure structure){
        //cannot be played if cost is greater than  current inspiration
        if(structure._structure.inspirationCost > CurrentInspiration)
        {
            Menu_Manager.instance.SetMessenger("Not enough inspiration to play this card");
            return false;
        }
        return true;
    }
    
    //called to end the turn of the player
    public void EndPlayerTurn()
    {   
        Board_Manager.instance.ActivateAllyStructureEndOfTurnEffects();
        instance.ChangeState(GameState.EnemiesTurn);
    }

    public void EndEnemyTurn(){
        Board_Manager.instance.ActivateEnemyStructureEndOfTurnEffects();
        instance.ChangeState(GameState.HeroesTurn);
    }

    public void StartPlayerTurn()
    {
        //increase and refresh inspiration
        IncreaseInspirationLimit();
        Draw(cardsDrawnPerTurn);
        CurrentInspiration = CurrentMaxInspiration;
        Menu_Manager.instance.UpdateIBar(CurrentInspiration, CurrentMaxInspiration, MaxInspiration);
        Event_Manager.instance.refresh();
        Debug.Log("Player has Started the Turn");
    }

    public void LoseGame(){
        Debug.Log("You Lost the game");
    }

    public void WinGame(){
        Debug.Log("You Win");
    }

    public void Draw(int amount){
        for(int i = 0; i < amount; i++){
            Card temp = deck.DrawCard();
            Menu_Manager.instance.SetSelectorToCard(temp);
        }
    }

    public void ShuffleDeck(){
        deck.ShuffleDeck();
    }

    public void LoadData(PlayerData playerData)
    {
        deck = playerData.GetDeck(playerData.SelectedDeck);
        Debug.Log("Deck Being Loaded: "+deck.name);
    }

    public void SaveData(ref PlayerData playerData)
    {
        int addedID = DataPersistanceManager.instance.idTable.getID(Board_Manager.instance.GetRewardCard());
        Debug.Log(addedID);
        if(GameWin){
            playerData._cardInventory.Add(addedID);
        }
    }

    private void PlayerPregameActions(){
        deck.RemoveCard(deck._leader);
        ShuffleDeck();
        Draw(StartingHandSize-cardsDrawnPerTurn);

    }

    private void ResolveGameWin(){
        Debug.Log("Resolving Game Win");
        GameWin = true;
        Menu_Manager.instance.showWinScreen(Board_Manager.instance.GetRewardCard());
        DataPersistanceManager.instance.SaveGame();
        
    } 
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHero = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4,

    GameWin = 5, 

    GameLoss = 6
}
