using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PauseMenuDeckSlot : MonoBehaviour
{
    [SerializeField] private Image LeaderImage;

    [SerializeField] private TextMeshProUGUI title; 

    [SerializeField] public GameObject SelectedFrame;

    private Deck CurrentlyHeldDeck;


    private void Awake(){
        SelectedFrame.SetActive(false);
    } 

    public void SetToDeck(Deck d){
        title.text = d.name;
        LeaderImage.sprite = d._leader.image;
        CurrentlyHeldDeck = d;
    }

    public void EditDeck(){
        Debug.Log("Opening the deck editor");
    }

    public void SelectDeck(){
        Debug.Log("Selecting Deck");
        SelectedFrame.SetActive(true);
    }
}
