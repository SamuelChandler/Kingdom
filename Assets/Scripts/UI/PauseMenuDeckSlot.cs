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

    [SerializeField] public GameObject SelectButton;

    [SerializeField] public GameObject EditButton;

    [SerializeField] public GameObject CreateDeckButton;

    private Deck CurrentlyHeldDeck;


    private void Awake(){
        SelectedFrame.SetActive(false);
    } 

    public void SetToDeck(Deck d){
        title.text = d.name;
        LeaderImage.color = new Color(255,255,255,255);
        LeaderImage.sprite = d._leader.image;
        CurrentlyHeldDeck = d;
        CreateDeckButton.SetActive(false);
        EditButton.SetActive(true);
        SelectButton.SetActive(true);
    }

    public void SetToEmpty(){
        title.text = "";
        LeaderImage.color = new Color(0,0,0,255);
        CurrentlyHeldDeck = null;
        CreateDeckButton.SetActive(true);
        EditButton.SetActive(false);
        SelectButton.SetActive(false);
    }

    public void EditDeck(){
        Debug.Log("Opening the deck editor");
        Scene_Manager.instance.GoToEditDeck(CurrentlyHeldDeck.name);
    }

    public void SelectDeck(){
        Debug.Log("Selecting Deck");
        Debug.Log(Player.instance.data.SelectedDeck);
        SelectedFrame.SetActive(true);

        if(CurrentlyHeldDeck != Player.instance.SelectedDeck){
            Player.instance.SetSelectedDeck(CurrentlyHeldDeck);
            Player.instance._pauseMenu.ShowDecks();
        }
        Debug.Log(Player.instance.data.SelectedDeck);
    }

    public void DeselectDeck(){
        SelectedFrame.SetActive(false);
    }

    public void CreateDeck(){
        Debug.Log("Creating Deck");
        Scene_Manager.instance.GoToCreatedeck();
    }
}
