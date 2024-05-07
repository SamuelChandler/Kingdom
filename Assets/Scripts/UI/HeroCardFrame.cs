using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroCardFrame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitName,cost,health,attack,description;
    [SerializeField] private Image unitImage;
    
    public void setCard(BaseHero h){
        unitName.text = h.unit.name;
        cost.text = h.unit.inspirationCost.ToString();
        health.text = h.unit.health.ToString();
        attack.text = h.unit.attack.ToString();
        unitImage.sprite = h.unit.image;
        
    }

    public void setCard(ScriptableUnit h){ 
        unitName.text = h.name;
        cost.text = h.inspirationCost.ToString();
        health.text = h.health.ToString();
        attack.text = h.attack.ToString();
        unitImage.sprite = h.image;
        description.text = h.description;
        
    }

    public void setCard(Card c){
        unitName.text = c.name;
        cost.text = c.inspirationCost.ToString();
        unitImage.sprite = c.image;
        description.text = c.description;
    }

}
