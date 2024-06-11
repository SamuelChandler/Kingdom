using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PauseMenuDeckSlot[] deckSlots;

    [SerializeField] private GameObject DeckView;

    public Deck SelectedDeck;
    public bool isPaused = false;


    void Awake(){
        gameObject.SetActive(false);
        DeckView.SetActive(false);
    }

    public void ShowDecks(string selectedname){

        //set view to active and get the list of decks
        DeckView.SetActive(true);
        List<Deck> decksToShow = Player.instance.data.GetAllDecks();

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
}
