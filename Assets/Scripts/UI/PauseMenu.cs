using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    
    void Awake(){
        gameObject.SetActive(false);
    }

    public void PauseGame(){
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame(){
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
