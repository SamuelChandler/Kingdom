using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile OccupiedTile;
    public ScriptableUnit unit;
    public SpriteRenderer spriteRenderer;

    [SerializeField] private TextMeshProUGUI attack, health;

    [SerializeField]  Material SelectedMaterial;

    [SerializeField] Material defualtMaterial;

    public bool isAbleToMove;
    public bool isAbleToAttack;

    public int currentHealth;
    public int currentMaxHealth;
    public int currentAttack;

    public int turnCounter;

    public virtual void Awake()
    {

         GetComponent<Renderer>().material = defualtMaterial; 

        spriteRenderer.sprite = unit.image;
        currentHealth = unit.health;
        currentMaxHealth = currentHealth;
        currentAttack = unit.attack;
        turnCounter = 0;

        if(unit._swift){
            isAbleToAttack = true;
            isAbleToMove = true;
        }else{
            isAbleToAttack = false;
            isAbleToMove = false;
        }
        

        attack.text = unit.attack.ToString();
        health.text = currentHealth.ToString();

        

    }


    void Refresh()
    {
        isAbleToMove=true;
        isAbleToAttack=true;
        turnCounter++;

        if(unit.OnStartOfTurn != null){
            unit.OnStartOfTurn.ActivateEffect(this);
        }
        
    }

    public void ActivateEndOfTurnEffects(){
        if(unit.OnEndTurn != null){
            unit.OnEndTurn.ActivateEffect(this);
        }
        
    }

    public void UpdateAttackAndHealthDisplay(){
        attack.text = currentAttack.ToString();
        health.text = currentHealth.ToString();

    }

    public void TakeDamage(int d){
        currentHealth = currentHealth-d;
        UpdateAttackAndHealthDisplay();

        if (currentHealth <= 0)
        {
            
            OccupiedTile.OccupiedUnit = null;
            removeUnit();
            Destroy(gameObject);

        }
    }

    public void Heal(int healAmount){
        currentHealth += healAmount;

        if(currentHealth > currentMaxHealth){
            currentHealth = currentMaxHealth;
        }

        UpdateAttackAndHealthDisplay();
    }

    public void Select(){
        GetComponent<Renderer>().material = SelectedMaterial; 

        Board_Manager.instance.ShowUnitActionTiles(this);

    }

    public void DeSelect(){
        GetComponent<Renderer>().material = defualtMaterial; 
        Board_Manager.instance.UnShowMovmentTiles(OccupiedTile,unit.speed);
    }

    public virtual void removeUnit(){}
}
