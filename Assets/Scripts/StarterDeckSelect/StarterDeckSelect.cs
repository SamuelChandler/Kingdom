using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarterDeckSelect : MonoBehaviour
{   
    //starter decks
    [SerializeField]
    public Deck windDeck;

    [SerializeField]
    protected Deck plantDeck;

    [SerializeField]
    protected Deck FireDeck;

    //selection indicators for the decks
    [SerializeField]
    protected Image Wind_Selector;

    [SerializeField]
    protected Image Plant_Selector;

    [SerializeField]
    protected Image Fire_Selector;

    [SerializeField]
    protected Button continueButton;
}
