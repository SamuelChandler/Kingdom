using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardSelectionWindow : MonoBehaviour
{

    [SerializeField]
    CardSelector[] _cardSelectors;

    [SerializeField]
    private int _numberOfCardsOnScreen;

    private Dictionary<Card,int> inventory;

    private List<Card> _currentSet;

    private int pageNumber;

    private void Awake(){
        
    }

    public void SetCards(List<Card> s){
        inventory = new Dictionary<Card, int>();

        foreach(Card c in s){
            inventory.Add(c,3);
        }
        ShowNonLeaders();
    }

    public void NextPage(){
        
        pageNumber++;

        for(int i = 0; i < _numberOfCardsOnScreen; i++){
            if(i + 8*pageNumber >= _currentSet.Count){
                _cardSelectors[i].gameObject.SetActive(false);
            }
            else{
                _cardSelectors[i].SetDisplayedCard(_currentSet[i+ 8*pageNumber],inventory[_currentSet[i+ 8*pageNumber]]);
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
                _cardSelectors[i].gameObject.SetActive(false);
            }
            else{
                _cardSelectors[i].SetDisplayedCard(_currentSet[i+ 8*pageNumber],inventory[_currentSet[i+ 8*pageNumber]]);
                
            }
        }

        
    }

    public void RefreshPage(){


        for(int i = 0; i < _numberOfCardsOnScreen; i++){
            if(i + 8*pageNumber >= _currentSet.Count){
                _cardSelectors[i].gameObject.SetActive(false);
            }
            else{
                _cardSelectors[i].SetDisplayedCard(_currentSet[i+ 8*pageNumber],inventory[_currentSet[i+ 8*pageNumber]]);
                
            }
        }

        
    }

    public void AddCard(Card c){
        Debug.Log("Adding "+c.name);
        inventory[c] ++;
        RefreshPage();
    }

    public void RemoveCard(Card c){
        Debug.Log("Removing "+c.name);
        inventory[c]--;
        RefreshPage();
    }

    public void ShowOnlyLeaders()
    {
        _currentSet = new List<Card>();
        foreach(Card c in inventory.Keys){
            if(c.type == CardType.Leader){
                _currentSet.Add(c);
            }
        }

        pageNumber = 0;

        for(int i = 0; i < _numberOfCardsOnScreen; i++){
            if(i >= _currentSet.Count){
                _cardSelectors[i].gameObject.SetActive(false);
            }else{
                _cardSelectors[i].SetDisplayedCard(_currentSet[i],inventory[_currentSet[i]]);
            }
        }
    }

    public void ShowNonLeaders(){
        _currentSet = new List<Card>();
        foreach(Card c in inventory.Keys){
            if(c.type != CardType.Leader){
                _currentSet.Add(c);
            }
        }

        pageNumber = 0;

        for(int i = 0; i < _numberOfCardsOnScreen; i++){
            //checking if at the end of list
            if(i >= _currentSet.Count){
                _cardSelectors[i].gameObject.SetActive(false);
            }else{
                _cardSelectors[i].SetDisplayedCard(_currentSet[i],inventory[_currentSet[i]]);
            }
            
        }
    }
}