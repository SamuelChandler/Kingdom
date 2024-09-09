using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckEntry : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    TextMeshProUGUI Name;
    [SerializeField]
    TextMeshProUGUI Cost;
    [SerializeField]
    public Card card;
    [SerializeField]
    TextMeshProUGUI Number;

    public void OnPointerClick(PointerEventData data){
        AudioManager.instance.Play("ButtonPress1");
        DeckBuilder.instance.RemoveCardFromDeck(card);
        RemoveCard();
    } 
    public void SetEntry(Card c){
        Name.text = c.name;
        Cost.text = c.inspirationCost.ToString();
        card = c;
        Number.text = "1";
    }

    public void RemoveCard(){
        if(Number.text == "3"){
            Number.text = "2";
        }else if(Number.text == "2"){
            Number.text = "1";
        }else if(Number.text == "1"){
            DeckBuilder.instance._dWindow.RemoveEntry(this);
           Destroy(gameObject);
        }
    }

    public void AddCard(){
        if(Number.text == "1"){
            Number.text = "2";
        }else if(Number.text == "2"){
            Number.text = "3";
        }
        
    }
    
}
