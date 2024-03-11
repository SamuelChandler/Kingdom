using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    private int Main_menu_index = 0;
    private int Game_index = 1;


    public void PlayGame()
    {
        SceneManager.LoadScene(Game_index);
    }
}
