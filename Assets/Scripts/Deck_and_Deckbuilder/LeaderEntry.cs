using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeaderEntry : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    TextMeshProUGUI Name;
    [SerializeField]
    public Card card;


    public void OnPointerClick(PointerEventData data){
        DeckBuilder.instance.RemoveCardFromDeck(card);
        RemoveCard();
    } 
    public void SetEntry(Card c){
        Name.text = c.name;
        card = c;
    }

    public void RemoveCard(){
        Name.text = "";
        card = null;
    }
}
    
