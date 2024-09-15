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

    public override IEnumerator PlayDamagedAnimation(int d)
    {
        float dur = Game_Manager.AttackDuration;

        float newValue = (float)currentHealth/(float)currentMaxHealth;

        float dif = HealthBar.value - newValue;

        float TimeElapsed = 0;

        while(TimeElapsed < dur){

            TimeElapsed += Time.deltaTime;

            HealthBar.value = newValue + dif*(1-(TimeElapsed/dur));

            yield return null;
        }

        RedBar.value = HealthBar.value;

        UpdateHealthDisplay();
    }
}