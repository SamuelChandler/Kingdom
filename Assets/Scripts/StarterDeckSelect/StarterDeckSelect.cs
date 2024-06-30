using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StarterDeckSelect : MonoBehaviour
{   
    //starter decks
    public ScriptableDeck windDeck;
    public ScriptableDeck plantDeck;
    public ScriptableDeck fireDeck;

    public ScriptableDeck SelectedDeck;

    public Color greyedColor;
    public Color ActiveColor;

    //selection indicators for the decks
    [SerializeField]
    protected Image Wind_Selector;

    [SerializeField]
    protected Image Plant_Selector;

    [SerializeField]
    protected Image Fire_Selector;

    [SerializeField]
    protected Button continueButton;

    public void Start(){
        GreyOutContinueButton();

        Wind_Selector.color = Color.clear;
        Plant_Selector.color = Color.clear;
        Fire_Selector.color = Color.clear;

    }

    public void SelectWindDeck(){
        SelectedDeck = windDeck;
        ActivateContinueButton();

        Wind_Selector.color = Color.white;
        Plant_Selector.color = Color.clear;
        Fire_Selector.color = Color.clear;
        
    }

    public void SelectPlantDeck(){
        SelectedDeck = plantDeck;
        ActivateContinueButton();

        Wind_Selector.color = Color.clear;
        Plant_Selector.color = Color.white;
        Fire_Selector.color = Color.clear;
    }

    public void SelectFireDeck(){
        SelectedDeck = fireDeck;
        ActivateContinueButton();

        Wind_Selector.color = Color.clear;
        Plant_Selector.color = Color.clear;
        Fire_Selector.color = Color.white;
    }

    public void GreyOutContinueButton(){
        continueButton.interactable = false;
        continueButton.GetComponent<Image>().color = greyedColor;
    }

    public void ActivateContinueButton(){
        continueButton.interactable = true;
        continueButton.GetComponent<Image>().color = ActiveColor;
    }

    public void Continue(){
        DataPersistanceManager.instance.playerData.AddStarterDeck(SelectedDeck);
        DataPersistanceManager.instance.SetEventCompleted(4,true);
        DataPersistanceManager.instance.SaveGame();
        Scene_Manager.instance.GoToOverworld();
    }
}
