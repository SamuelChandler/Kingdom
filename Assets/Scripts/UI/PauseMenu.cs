using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject DeckSlotPrefab;

    [SerializeField] private GameObject DeckView;


    void Awake(){
        gameObject.SetActive(false);
        DeckView.SetActive(false);
    }

    public void ShowDecks(){
        DeckView.SetActive(true);
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
