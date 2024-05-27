using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    HeroCardFrame _cardframe;
    Card _card;

    [SerializeField] public GameObject[] increment;

    public int Amount = 3;

    [SerializeField] public Color selectedColor;
    [SerializeField] public Color unUsedColor;

    public void Awake(){
        selectedColor = Color.grey;
        unUsedColor = Color.white;
    }

    public void SetDisplayedCard(Card c, int a){
        if(c != null){
            _card = c;
            _cardframe.setCard(_card);
            gameObject.SetActive(true);

            Amount = a;

            for(int i = 0; i < a; i++){
                
                increment[i].GetComponent<Image>().color = unUsedColor;
            }
            for(int i = a; i < 3; i ++ ){
                increment[i].GetComponent<Image>().color = selectedColor;
            }

        }else{
            gameObject.SetActive(false);
        }
        
    }


    public void OnPointerClick(PointerEventData data){
        if(Amount == 0){
            return;
        }
        DeckBuilder.instance.AddCardToDeck(_card);
    }

    
}
