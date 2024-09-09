using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossScreen : MonoBehaviour
{
    void Awake(){
        gameObject.SetActive(false);

    }

    public void ShowLossScreen(){
        gameObject.SetActive(true);
    } 

    public void Continue(){
        AudioManager.instance.Play("ButtonPress1");
        Scene_Manager.instance.GoToOverworld();
    
    }
}
