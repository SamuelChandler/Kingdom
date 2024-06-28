using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public void SelectWindDeck(){
        SelectedDeck = windDeck;
        ActivateContinueButton();
        
    }

    public void SelectPlantDeck(){
        SelectedDeck = plantDeck;
        ActivateContinueButton();
    }

    public void SelectFireDeck(){
        SelectedDeck = fireDeck;
        ActivateContinueButton();
    }

    public void GreyOutContinueButton(){
        continueButton.interactable = false;
        continueButton.GetComponent<Image>().color = greyedColor;
    }

    public void ActivateContinueButton(){
        continueButton.interactable = true;
        continueButton.GetComponent<Image>().color = ActiveColor;
    }
}
