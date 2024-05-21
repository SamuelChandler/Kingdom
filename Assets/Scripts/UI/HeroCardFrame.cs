using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroCardFrame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitName,cost,health,attack,description;
    [SerializeField] private Image unitImage;
    [SerializeField] private Image _backgroundImage;

    [SerializeField] private Sprite spellBG,unitBG,structureBG;
    
    public void setCard(BaseHero h){
        unitName.text = h.unit.name;
        _backgroundImage.sprite = unitBG;
        cost.text = h.unit.inspirationCost.ToString();
        health.text = h.unit.health.ToString();
        attack.text = h.unit.attack.ToString();
        unitImage.sprite = h.unit.image;
        
    }

    public void setCard(ScriptableUnit h){ 
        unitName.text = h.name;
        _backgroundImage.sprite = unitBG;
        cost.text = h.inspirationCost.ToString();
        health.text = h.health.ToString();
        attack.text = h.attack.ToString();
        unitImage.sprite = h.image;
        description.text = h.description;
        
    }

    public void setCard(Spell s){
        unitName.text = s.name;
        _backgroundImage.sprite = spellBG;
        cost.text = s.inspirationCost.ToString();
        health.text = "";
        attack.text = "";
        unitImage.sprite = s.image;
        description.text = s.description;
    }

    public void setCard(ScriptableStructure s){
        unitName.text = s.name;
        _backgroundImage.sprite = structureBG;
        cost.text = s.inspirationCost.ToString();
        health.text = s.health.ToString();
        attack.text = "";
        unitImage.sprite = s.image;
        description.text = s.description;
    }

    public void setCard(Card c){

        if(c.type == CardType.Unit || c.type == CardType.Leader){
            setCard((ScriptableUnit)c);
            return;
        }else if(c.type == CardType.Spell){
            setCard((Spell)c);
            return;
        }else if(c.type == CardType.Structure){
            setCard((ScriptableStructure)c);
            return;
        }
        
        unitName.text = c.name;
        cost.text = c.inspirationCost.ToString();
        unitImage.sprite = c.image;
        description.text = c.description;
    }

}
