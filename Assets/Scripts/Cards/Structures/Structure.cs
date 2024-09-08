using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//base class for all structure types 
public class Structure : BoardObject
{

    [SerializeField]
    public ScriptableStructure _structure;

    [SerializeField]
    public SpriteRenderer spriteRenderer;

    public int turnCounter;


    protected virtual void Awake()
    {
        card = _structure;
        faction = _structure.Faction;

        spriteRenderer.sprite = _structure.image;
        currentHealth = _structure.health;
        currentMaxHealth = currentHealth;
        HealthBar.value = 1;
        turnCounter = 0;

        SetBuffs();

        if(_structure.Faction == Faction.Hero && _structure.OnAllyDeath != null){
            Event_Manager.onAllyDeath += _structure.OnAllyDeath.ActivateEffect;
        }
    }

    public void Refresh(){
        turnCounter++;

        if(_structure.StartOfTurn != null){
            _structure.StartOfTurn.ActivateEffect(this);
        }
        
    }

    public void ActivateEndOfTurnEffects(){
        if(_structure.EndOfTurn != null){
            _structure.EndOfTurn.ActivateEffect(this);
        }
    }

    
    public void ReverseDamagePreview(){
        UpdateHealthDisplay();
    }

    public void SetBuffs(){
        if(_structure.Faction == Faction.Hero){
            Board_Manager.instance.allyAttackBuff += _structure.allyAttackBoost;
        }
        else if(_structure.Faction == Faction.Enemy){
            Board_Manager.instance.enemyAttackBuff += _structure.allyAttackBoost;
        }else{
            Board_Manager.instance.allyAttackBuff += _structure.allyAttackBoost;
            Board_Manager.instance.enemyAttackBuff += _structure.allyAttackBoost;
        }
        Board_Manager.instance.ClearFieldBuffs();
        Board_Manager.instance.ApplyFieldBuffs();
    }

    public void ClearBuff(){
        if(_structure.Faction == Faction.Hero){
            Board_Manager.instance.allyAttackBuff -= _structure.allyAttackBoost;
        }
        else if(_structure.Faction == Faction.Enemy){
            Board_Manager.instance.enemyAttackBuff -= _structure.allyAttackBoost;
        }else{
            Board_Manager.instance.allyAttackBuff -= _structure.allyAttackBoost;
            Board_Manager.instance.enemyAttackBuff -= _structure.allyAttackBoost;
        }

        Board_Manager.instance.ClearFieldBuffs();
        Board_Manager.instance.ApplyFieldBuffs();
    }

    override public void TakeDamage(int d){
        currentHealth = currentHealth-d;

        if (currentHealth <= 0)
        {

            if(_structure.WhenDestroyed != null){
                _structure.WhenDestroyed.ActivateEffect(this);
            }

            OccupiedTile.OccupiedObject = null;

            removeStructure();

        }
    }

    

    public virtual void removeStructure(){}


}
