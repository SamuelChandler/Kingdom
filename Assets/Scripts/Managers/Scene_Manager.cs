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
        SceneManager.LoadScene(3);
    }

    public void GoToBattle(){
        PlayerPrefs.SetInt("Level Selected", 69);
        SceneManager.LoadScene(2);
    }

    public void StartNewGame(){
        DataPersistanceManager.instance.NewGame("NewPlayer");
        DataPersistanceManager.instance.SaveGame();

        GoToOverworld();
    }



    
}
