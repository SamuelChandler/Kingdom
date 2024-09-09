using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PauseMenuDeckSlot[] deckSlots;

    [SerializeField] private GameObject DeckView;
    [SerializeField] private GameObject CardView;
    [SerializeField] private CardViewScreen CV;
    [SerializeField] private GameObject InventoryView;
    [SerializeField] private GameObject SettingsView;

    public Deck SelectedDeck;
    public bool isPaused;


    void Awake(){
        gameObject.SetActive(false);

        ClearSubview();
        
        isPaused = false;
    }

    public void ShowDecks(string selectedname){

        if(selectedname== ""){
            selectedname = Player.instance.SelectedDeck.name;
        }


        ClearSubview();

        //set view to active and get the list of decks
        DeckView.SetActive(true);

        List<Deck> decksToShow = new List<Deck>();

        if(Player.instance != null){
            decksToShow = Player.instance.data.GetAllDecks();
        }
        

        //Set each view to the deck selected 
        for(int i = 0 ; i < deckSlots.Length; i++){
            if(i >= decksToShow.Count){
                deckSlots[i].SetToEmpty();
            }else{
                deckSlots[i].SetToDeck(decksToShow[i]);

                Debug.Log(decksToShow[i].name);

                if(decksToShow[i].name == selectedname){
                    deckSlots[i].SelectedFrame.SetActive(true);
                }else{
                    deckSlots[i].SelectedFrame.SetActive(false);
                }
            }

        }

        
    }

    public void ShowCardView(){
        AudioManager.instance.Play("ButtonPress2");
        ClearSubview();
        CardView.SetActive(true);
        CV.SetCards(DataPersistanceManager.instance.playerData.GetInventory());
        CV.ShowCards();
        
    }

    public void ShowSettings(){
        AudioManager.instance.Play("ButtonPress2");

        ClearSubview();

        SettingsView.SetActive(true);
        
    }

    public void ShowInventory(){
        AudioManager.instance.Play("ButtonPress2");

        ClearSubview();

        InventoryView.SetActive(true);
        
    }

    public void SetMusicVol(Slider s){
        AudioManager.instance.SetMusicVol(s.value);
    }

    public void SetEffectVol(Slider s){
        AudioManager.instance.SetSfxVol(s.value);
        AudioManager.instance.Play("ButtonPress1");
    }

    public void PlayButton2(){
        AudioManager.instance.Play("ButtonPress2");
    }


    // Clears all the subviews in the current 
    public void ClearSubview(){
        DeckView.SetActive(false);
        SettingsView.SetActive(false);
        InventoryView.SetActive(false);
        CardView.SetActive(false);
    }

    public void PauseGame(){
        if (!isPaused)
        {
            isPaused = true;
            AudioManager.instance.Play("Pause");
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ContinueGame(){
        if (isPaused)
        {
            isPaused = false;
            AudioManager.instance.Play("UnPause");
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void QuitGame(){
        AudioManager.instance.Play("ButtonPress2");
        DataPersistanceManager.instance.SaveGame();
        Application.Quit();
    }
}
