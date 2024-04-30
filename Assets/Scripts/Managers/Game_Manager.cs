using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    public GameState GameState;
    private int Level;

    public EnemyAI eAI;

    public int CurrentInspiration;
    [SerializeField] private int CurrentMaxInspiration; 
    private int MaxInspiration = 10;

    private void Awake()
    {
        instance = this;
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
                Board_Manager.instance.SpawnLeader();
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
    public bool CanBePlayed(BaseUnit unit)
    {
        //cannot be played if cost is greater than  current inspiration
        if(unit.unit.inspirationCost > CurrentInspiration)
        {
            Debug.Log("Not enough inspiration to play this card");
            return false;
        }
        return true;
    }

    public bool CanBePlayed(AllyStructure structure){
        //cannot be played if cost is greater than  current inspiration
        if(structure._structure.inspirationCost > CurrentInspiration)
        {
            Debug.Log("Not enough inspiration to play this card");
            return false;
        }
        return true;
    }

    //called to end the turn of the player
    public void EndPlayerTurn()
    {
        Debug.Log("Player has ended the Turn");
        instance.ChangeState(GameState.EnemiesTurn);
    }

    public void StartPlayerTurn()
    {
        //increase and refresh inspiration
        IncreaseInspirationLimit();
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

    
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHero = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4
}
