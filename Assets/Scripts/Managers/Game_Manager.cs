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

    private int CurrentInspiration;
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

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                Board_Manager.instance.generateGrid();
                break;
            case GameState.SpawnHero:
                Unit_Manager.instance.SpawnHeros();
                break;
            case GameState.SpawnEnemies:
                Unit_Manager.instance.SpawnEnemies();
                break;
            case GameState.HeroesTurn:

                break;
            case GameState.EnemiesTurn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
    }
    

    public void IncreaseInsperationLimit()
    {
        
        CurrentMaxInspiration += 1;
        
        if (CurrentMaxInspiration > MaxInspiration)
        {
            CurrentMaxInspiration -= 1;
        }

        Menu_Manager.instance.UpdateIBar(CurrentInspiration, CurrentMaxInspiration, MaxInspiration);
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
