using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelector : MonoBehaviour
{
    public ScriptableUnit held_unit;
    private Button _button;
    private Image _image;
    //[SerializeField] protected Image _image;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(btnClick);

        _image = GetComponent<Image>();

        if (held_unit.image != null)
        {
            _image.sprite = held_unit.image;
        }
    }

    //in hover should show full unit info 
    private void OnMouseEnter()
    {
        Menu_Manager.instance.SetMessenger("hovering");
        Menu_Manager.instance.showSelectedHero(held_unit);
    }
    private void OnMouseLeave()
    {
        Menu_Manager.instance.showSelectedHero((BaseHero)null);
    }

    void btnClick()
    {
        //Debug.Log("unit selector pressed");
        if (held_unit == null) return;
        
        
        //var createdUnit = Instantiate(held_unit);
        Unit_Manager.instance.SetSelectedHero(held_unit);
    }

    void setImage(Sprite s)
    {
        _image.sprite = s;
    }
}
