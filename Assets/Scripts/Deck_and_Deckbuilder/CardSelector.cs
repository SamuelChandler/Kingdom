using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    HeroCardFrame _cardframe;
    Card _card;

    public void SetDisplayedCard(Card c){
        if(c != null){
            _card = c;
            _cardframe.setCard(_card);
            gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
        }
        
    }

    public void OnPointerClick(PointerEventData data){
        Debug.Log(_card.name);
        AddCard(_card);
    }

    public void AddCard(Card card){
        DeckBuilder.instance.AddCardToDeck(card);
    }

    public void saveInventory(){
        
    }
}
