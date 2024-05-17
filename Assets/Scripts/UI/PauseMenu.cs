using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PauseMenuDeckSlot[] deckSlots;

    [SerializeField] private GameObject DeckView;

    public Deck SelectedDeck;


    void Awake(){
        gameObject.SetActive(false);
        DeckView.SetActive(false);
    }

    public void ShowDecks(){

        //set view to active and get the list of decks
        DeckView.SetActive(true);
        List<Deck> decksToShow = Player.instance.data.GetAllDecks();

        //Set each view to the deck selected 
        for(int i = 0 ; i < deckSlots.Length; i++){
            if(i >= decksToShow.Count){
                deckSlots[i].SetToEmpty();
            }else{
                deckSlots[i].SetToDeck(decksToShow[i]);

                if(decksToShow[i] == Player.instance.SelectedDeck){
                    deckSlots[i].SelectDeck();
                }else{
                    deckSlots[i].DeselectDeck();
                }
            }

        }
    }

    public void PauseGame(){
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame(){
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
