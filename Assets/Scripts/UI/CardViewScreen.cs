using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardViewScreen : MonoBehaviour
{
    [SerializeField] HeroCardFrame[] _cards;

    [SerializeField] private int _numberOfCardsOnScreen;

    [SerializeField] private Dictionary<Card,int> inventory;

    [SerializeField] private List<Card> _currentSet;

    [SerializeField] private int pageNumber;

    public void SetCards(List<Card> s){
        inventory = new Dictionary<Card, int>();

        foreach(Card c in s){
            inventory.Add(c,3);
        }
        ShowCards();
    }

    public void ShowCards(){
        _currentSet = new List<Card>();

        foreach(Card c in inventory.Keys){ 
            _currentSet.Add(c); 
        }

        pageNumber = 0;

        for(int i = 0; i < _numberOfCardsOnScreen; i++){
            //checking if at the end of list
            if(i >= _currentSet.Count){
                _cards[i].setInvisible();
            }else{
                _cards[i].setCard(_currentSet[i]);
            }
            
        }
    }

    public void NextPage(){
        
        pageNumber++;

        for(int i = 0; i < _numberOfCardsOnScreen; i++){
            if(i + 8*pageNumber >= _currentSet.Count){
                _cards[i].setInvisible();
            }
            else{
                _cards[i].setCard(_currentSet[i+ 8*pageNumber]);
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
            if(i + 8*pageNumber >= _currentSet.Count){
                _cards[i].setInvisible();
            }
            else{
                _cards[i].setCard(_currentSet[i+ 8*pageNumber]);
                
            }
        }
    }
}