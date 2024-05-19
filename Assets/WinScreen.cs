using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public HeroCardFrame cardFrame;

    void Awake(){
        gameObject.SetActive(false);

    }

    public void ShowWinScreen(Card c){
        gameObject.SetActive(true);
        cardFrame.setCard(c);
    }


    //should return the user to the overworld saving of data is done before this point
    public void Continue(){
        Debug.Log("Continue Pressed");
        Scene_Manager.instance.GoToOverworld();
    }
}
