using System.Collections.Generic;
using UnityEngine;
using System;


public class Game_Manager : MonoBehaviour,IDataPersistance
{
    public static Game_Manager instance;
    public GameState GameState;
    private int Level;

    [SerializeField] public static float MoveDuration = 0.5f;
    [SerializeField] public static float AttackDuration = 1f;

    public EnemyAI eAI;

    public int CurrentInspiration;
    [SerializeField] private int CurrentMaxInspiration; 
    private int MaxInspiration = 10;

    //feilds relating to drawing cards and hand size
    [SerializeField] private int cardsDrawnPerTurn = 1;
    [SerializeField] private int StartingHandSize = 3;

    private bool GameWin;

    private Deck deck;

    private int _turn;

    private List<Spawner> spawners;

    private bool TimeLimit;

    private void Awake()
    {
        instance = this;
        GameWin = false;
        _turn = 0;
        CurrentInspiration = CurrentMaxInspiration;

    }

    private void Start()
    {
        //start of game menu manager updates with game info
        Menu_Manager.instance.UpdateIBar(CurrentInspiration, CurrentMaxInspiration, MaxInspiration);
    }

    public void StartGame(){

        //determine if a time limit for turns is instituted
        if(Board_Manager.instance._map._turns > 0){
            TimeLimit = true;
        }else{
            TimeLimit = false;
        }

        //Start Generating the Map
        ChangeState(GameState.GenerateMap);

    }

    //used to change the state of the game 
    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateMap:
                Debug.Log("Generating Map");
                Board_Manager.instance.generateGrid();
                Board_Manager.instance.SpawnMapStructures();
                SetMusic();
                SetSpawners();
                break;
            case GameState.SpawnHero:
                Debug.Log("Spawning Hero");
                Board_Manager.instance.SpawnLeader((ScriptableUnit)deck._leader);
                PlayerPregameActions();
                ChangeState(GameState.SpawnEnemies);
                break;
            case GameState.SpawnEnemies:
                Debug.Log("Spawning Enemies");
                Board_Manager.instance.SpawnBoss();
                Unit_Manager.instance.SpawnEnemies();
                OnEnemySpawn();
                break;
            case GameState.HeroesTurn:
                Debug.Log("Player Turn");
                StartPlayerTurn();
                break;
            case GameState.EnemiesTurn:
                ResolveSpawners();
                eAI.StartTurn();
                break;
            case GameState.GameWin:
                ResolveGameWin();
                break;
            case GameState.GameLoss:
                ResolveGameLoss();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
    }

    public void OnEnemySpawn(){
        if(Board_Manager.instance._map._levelType == LevelType.DefeatBoss){
            string BossName = Board_Manager.instance._map._boss.enemy.name;
            Menu_Manager.instance.showGameGoal("Defeat: " + BossName);
        }
    }
    
    private void SetSpawners(){

        spawners = new List<Spawner>();

        foreach(Spawner s in Board_Manager.instance._map._spawners){
            spawners.Add(s);
        }
    }

    private void ResolveSpawners(){
        foreach(Spawner s in spawners){
            if(_turn % s.turnsForSpawnToOccur == 0){
                //if spawner timer is up
                Vector2 dest = s.posLocations[UnityEngine.Random.Range(0,s.posLocations.Length)];
                Tile destTile = Board_Manager.instance.GetTileAtPosition(dest);
                Board_Manager.instance.SpawnUnit(destTile, s.unit);
            }
        }
    }

    public void ResolveNoNeutralStructures(){
        if(Board_Manager.instance._map._levelType == LevelType.ClearAllNeutralEnemiesInTimeLimit){
            ChangeState(GameState.GameWin);
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

        foreach (BoardObject a in Board_Manager.instance.allyBoardObjects){
            if(Board_Manager.instance.WithinOne(a.OccupiedTile,dest)){
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
            if(t.OccupiedObject == null){
                Menu_Manager.instance.SetMessenger("there is not a target in this location");
                return false;   
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
        //clear all indicators 
        Board_Manager.instance.ClearBoardIndicators();

        //activate end of turn effects
        Board_Manager.instance.ActivateAllyStructureEndOfTurnEffects();
        Board_Manager.instance.TileEndOfTurnEffects();

        //change game state
        instance.ChangeState(GameState.EnemiesTurn);
    }

    public void EndEnemyTurn(){
        Board_Manager.instance.ActivateEnemyStructureEndOfTurnEffects();
        instance.ChangeState(GameState.HeroesTurn);
    }

    public void StartPlayerTurn()
    {
        //increase turn number
        _turn++;

        //check if in survival mode and if the player has won the game
        SurvivalTurnCheck();

        //check if the User has run out of Time 
        TimeLimitCheck();
        
        //increase and refresh inspiration
        IncreaseInspirationLimit();
        Draw(cardsDrawnPerTurn);
        CurrentInspiration = CurrentMaxInspiration;
        Menu_Manager.instance.UpdateIBar(CurrentInspiration, CurrentMaxInspiration, MaxInspiration);
        Event_Manager.instance.refresh();

    }

    public void TimeLimitCheck(){

        //do nothing if there is not a time limit
        if(!TimeLimit){return;}

        //do nothing if the type of Game Mode is survival
        if(Board_Manager.instance._map._levelType == LevelType.Survive)return;

        string goalString = "Turns Left = " + (Board_Manager.instance._map._turns - _turn +1).ToString();
        Menu_Manager.instance.showGameGoal(goalString);

        if(Board_Manager.instance._map._turns < _turn){
            ChangeState(GameState.GameLoss);
        }
    }

    public void SurvivalTurnCheck(){
        //do nothing if it is not a survival map
        if(Board_Manager.instance._map._levelType != LevelType.Survive){return;}

        string goalString = "Turns Left to Survive = " + (Board_Manager.instance._map._turns - _turn +1).ToString();
        Menu_Manager.instance.showGameGoal(goalString);

        if(Board_Manager.instance._map._turns < _turn){
            ChangeState(GameState.GameWin);
        }
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
        if(GameWin){
            playerData.AddCardToInventory(Board_Manager.instance.GetRewardCard());
            playerData.SetEventCompleted(Board_Manager.instance._map.onWinEvent);
        }
    }

    private void PlayerPregameActions(){
        deck.RemoveCard(deck._leader);
        ShuffleDeck();
        Draw(StartingHandSize-cardsDrawnPerTurn);

    }

    private void SetMusic()
    {
        bool eliteBattle = Board_Manager.instance._map.eliteBattle;

        if(eliteBattle){
            AudioManager.instance.Play("EliteBattleMusic");
        }else{
            AudioManager.instance.Play("BasicBattleMusic");
        }
    }

    private void ResolveGameWin(){
        Debug.Log("Resolving Game Win");
        GameWin = true;
        Menu_Manager.instance.showWinScreen(Board_Manager.instance.GetRewardCard());
        DataPersistanceManager.instance.SaveGame();
    } 

    private void ResolveGameLoss(){
        Debug.Log("Resolving game Loss");
        GameWin = false;
        Menu_Manager.instance.showLossScreen();
        DataPersistanceManager.instance.SaveGame();
    }

    internal void CheckEnemyClearCondition()
    {
        //do nothing if it is not a map that cares about 
        if(Board_Manager.instance._map._levelType != LevelType.ClearEnemies){return;}

        //do nothing if an enemy unit still exists 
        if(Board_Manager.instance._enemies.Count != 0){return;}

        //do nothing if an enemy structure still exists
        if(Board_Manager.instance._EnemyStructures.Count != 0){return;}

        //passed All checks resolve Win 
        ChangeState(GameState.GameWin);
    }
}

public enum GameState
{
    GenerateMap = 0,
    SpawnHero = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4,

    GameWin = 5, 

    GameLoss = 6
}
