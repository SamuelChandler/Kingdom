/*
 * Manages the interaction with the game and the different scenes in the Game
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    private int Level_Select_index = 1;
    private int CombatMap_index = 2;

    private int OverWorld = 3;

    private int Deck_Builder_index = 4;

    private int StarterSelect = 5;

    public static Scene_Manager instance;


    private void Awake(){
        instance = this;
    }

    //Loads the Load Level Scene
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene(Level_Select_index);
    }

    //loads the overworld scene
    public void GoToOverworld(){
        SceneManager.LoadScene(OverWorld);
        AudioManager.instance.Play("Music");
    }

    public void GoToBattle(ScriptableMap map){
        Debug.Log("going to fight" + map.name);
        Player.instance.data.CombatMap = DataPersistanceManager.instance.mapIDTable.getID(map);
        DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadScene(CombatMap_index);
    }

    //goes to the deckbuilder with the name of the deck being created
    public void GoToEditDeck(string deckname){
        DataPersistanceManager.instance.SaveGame();
        PlayerPrefs.SetString("DeckToBeEdited", deckname);
        SceneManager.LoadScene(Deck_Builder_index);
    }

    public void GoToCreatedeck(){
        DataPersistanceManager.instance.SaveGame();
        PlayerPrefs.SetString("DeckToBeEdited", null);
        SceneManager.LoadScene(Deck_Builder_index);
    }

    public void StartNewGame(){
        DataPersistanceManager.instance.NewGame("NewPlayer");
        DataPersistanceManager.instance.SaveGame();
        GoToOverworld();
    }

    public void StartSuperGame(){
        DataPersistanceManager.instance.SuperGame("NewPlayer");
        DataPersistanceManager.instance.SaveGame();
        GoToOverworld();
    }

    public void goToOverWorldFromDeckBuilder(){
        PlayerPrefs.SetString("FromDeckbuiler","Yes");
        GoToOverworld();
    }

    public void GoToStarterSelect(){
        DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadScene(StarterSelect);
    }
}