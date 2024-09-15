using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStructure : Structure
{

    protected override void Awake(){
        card = _structure;
        faction = _structure.Faction;

        spriteRenderer.sprite = _structure.image;
        currentHealth = _structure.health;
        currentMaxHealth = currentHealth;
        HealthBar.value = 1;
        turnCounter = 0;

        SetBuffs();
        
        Event_Manager.OnRefresh += Refresh;

        if(_structure.Faction == Faction.Hero && _structure.OnAllyDeath != null){
            Event_Manager.onAllyDeath += _structure.OnAllyDeath.ActivateEffect;
        }
    }

    public override void removeStructure(){
        ClearBuff();
        Board_Manager.instance.removeAllyStructure(this);
    }
}