using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelector : MonoBehaviour
{
    public Card held_card;
    private Button _button;
    private Image _image;
    //[SerializeField] protected Image _image;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(btnClick);

        _image = GetComponent<Image>();

    }

    //in hover should show full unit info 
    private void OnMouseEnter()
    {
        Menu_Manager.instance.SetMessenger("hovering");
        
        Menu_Manager.instance.showCard(held_card);
        
        
    }
    private void OnMouseLeave()
    {
        Menu_Manager.instance.showCard(null);
    }

    void btnClick()
    {
        
        if (held_card == null) return;
        

        Unit_Manager.instance.SetSelectedCard(held_card);
        Menu_Manager.instance.CurrentSelectedSelector = this;
    }

    void setImage(Sprite s)
    {
        _image.sprite = s;
    }

    public void SetCard(Card c){

        if(c == null){
            Debug.Log("Cannot Set A null Card");
            return;
        }

        held_card = c;

        if (held_card.image != null)
        {
            _image.sprite = held_card.image;
        }
    }

    public void ClearCard(){
        held_card = null;
        _image.sprite = null;
    }
}
