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
        SelectedFrame.SetActive(false);
        CreateDeckButton.SetActive(true);
        EditButton.SetActive(false);
        SelectButton.SetActive(false);
    }

    public void EditDeck(){
        AudioManager.instance.Play("ButtonPress1");
        Debug.Log("Opening the deck editor");
        Scene_Manager.instance.GoToEditDeck(CurrentlyHeldDeck.name);
    }

    public void SelectDeck(){
        Debug.Log("Selecting Deck");
        Debug.Log(Player.instance.data.SelectedDeck);  

        if(CurrentlyHeldDeck.name != Player.instance.data.SelectedDeck){
            Player.instance.SetSelectedDeck(CurrentlyHeldDeck);
            SelectedFrame.SetActive(true);
            Debug.Log(Player.instance.data.SelectedDeck);
            AudioManager.instance.Play("ButtonPress1");
        }
        
    }

    public void CreateDeck(){
        AudioManager.instance.Play("ButtonPress1");
        Debug.Log("Creating Deck");
        Scene_Manager.instance.GoToCreatedeck();
    }
}
