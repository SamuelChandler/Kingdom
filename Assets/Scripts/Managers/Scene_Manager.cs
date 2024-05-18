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
    private int Game_index = 2;

    private int OverWorld = 3;

    private int Deck_Builder_index = 4;

    public static Scene_Manager instance;


    private void Awake(){
        instance = this;
    }
    
    //Goes to the Game scene and defines it the level integer
    public void GoToGame(int level)
    {
        PlayerPrefs.SetInt("Level Selected", level);
        SceneManager.LoadScene(Game_index);
    }

    //Loads the Load Level Scene
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene(Level_Select_index);
    }

    //loads the overworld scene
    public void GoToOverworld(){
        SceneManager.LoadScene(OverWorld);
    }

    public void GoToBattle(){
        DataPersistanceManager.instance.SaveGame();
        PlayerPrefs.SetInt("Level Selected", 69);
        SceneManager.LoadScene(2);
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
        SceneManager.LoadScene(OverWorld);
    }

    public void goToOverWorldFromDeckBuilder(){
        PlayerPrefs.SetString("FromDeckbuiler","Yes");
        SceneManager.LoadScene(OverWorld);
    }



    
}
