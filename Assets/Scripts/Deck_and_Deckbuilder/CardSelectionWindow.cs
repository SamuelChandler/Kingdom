using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardSelectionWindow : MonoBehaviour
{

    [SerializeField]
    HeroCardFrame[] _cards;

    [SerializeField]
    private int _numberOfCardsOnScreen;

    private Set _currentSet;

    private int pageNumber;

    private void Awake(){
        
    }

    public void SetCards(Set s){

        _currentSet = s;
        pageNumber = 0;

        for(int i = 0; i < _numberOfCardsOnScreen; i++){
            if((ScriptableUnit)_currentSet.CardsInSet[i]){

                _cards[i].gameObject.SetActive(true);
                _cards[i].setCard((ScriptableUnit)_currentSet.CardsInSet[i]);

            }else if(_currentSet.CardsInSet[i]){

                _cards[i].gameObject.SetActive(true);
                _cards[i].setCard(_currentSet.CardsInSet[i]);

            }else{

                _cards[i].gameObject.SetActive(false);

            }
        }

    
    }

    public void NextPage(){
        
        pageNumber++;

        for(int i = 0; i < _numberOfCardsOnScreen; i++){
            if(i + 8*pageNumber >= _currentSet.CardsInSet.Count){
                _cards[i].gameObject.SetActive(false);
            }
            else if((ScriptableUnit)_currentSet.CardsInSet[i + 8*pageNumber] ){

                _cards[i].gameObject.SetActive(true);
                _cards[i].setCard((ScriptableUnit)_currentSet.CardsInSet[i+ 8*pageNumber] );

            }else{

                _cards[i].gameObject.SetActive(true);
                _cards[i].setCard(_currentSet.CardsInSet[i+ 8*pageNumber]);
                
            }
        }

        
    }

    public void PreviosPage(){
        
        pageNumber--;

        if(pageNumber < 0){
            Debug.Log("Cannot Go back a page");
            pageNumber = 0;
            return;
        }

        for(int i = 0; i < _numberOfCardsOnScreen; i++){
            if(i + 8*pageNumber >= _currentSet.CardsInSet.Count){
                _cards[i].gameObject.SetActive(false);
            }
            else if((ScriptableUnit)_currentSet.CardsInSet[i + 8*pageNumber] ){

                _cards[i].gameObject.SetActive(true);
                _cards[i].setCard((ScriptableUnit)_currentSet.CardsInSet[i+ 8*pageNumber] );

            }else{

                _cards[i].gameObject.SetActive(true);
                _cards[i].setCard(_currentSet.CardsInSet[i+ 8*pageNumber]);
                
            }
        }

        
    }
}