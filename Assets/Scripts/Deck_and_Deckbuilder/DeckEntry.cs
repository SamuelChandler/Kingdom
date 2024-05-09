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
    Card card;
    [SerializeField]
    TextMeshProUGUI Number;

    public void OnPointerClick(PointerEventData data){
        
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
