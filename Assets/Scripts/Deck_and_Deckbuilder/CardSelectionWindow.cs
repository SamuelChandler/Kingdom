using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionWindow : MonoBehaviour
{

    [SerializeField]
    HeroCardFrame[] _cards;

    [SerializeField]
    private int _numberOfCardsOnScreen;

    private Set _currentSet;

    private void Awake(){
        
    }

    public void SetCards(Set s){

        _currentSet = s;

        int currentCardindex = 0;

        foreach ( Card c in s.CardsInSet){

            if((ScriptableUnit)c)
                _cards[currentCardindex].setCard((ScriptableUnit)c);
            else{
                _cards[currentCardindex].setCard(c);
            }

            currentCardindex++;
            if(currentCardindex >= _numberOfCardsOnScreen){
                break;
            }
        }
    }
}