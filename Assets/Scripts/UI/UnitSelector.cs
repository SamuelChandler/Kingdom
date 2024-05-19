using System.Collections;
using System.Collections.Generic;
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
        if(held_card.type == CardType.Unit){
            Menu_Manager.instance.showUnit((ScriptableUnit)held_card);
        }
        
    }
    private void OnMouseLeave()
    {
        Menu_Manager.instance.showUnit((BaseHero)null);
    }

    void btnClick()
    {
        
        if (held_card == null) return;
        
        //var createdUnit = Instantiate(held_unit);
        if(held_card.type == CardType.Unit){
            Unit_Manager.instance.SetSelectedHero((ScriptableUnit)held_card);
        }
        
    }

    void setImage(Sprite s)
    {
        _image.sprite = s;
    }

    public void SetCard(Card c){
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
