using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BaseUnit : BoardObject
{
    
    public ScriptableUnit unit;

    //animation and art Stuff
    public SpriteRenderer spriteRenderer;

    [SerializeField] private TextMeshProUGUI attack, health;

    [SerializeField]  Material SelectedMaterial;

    [SerializeField] Material defualtMaterial;

    Animator animator;

    public bool isAbleToMove;
    public bool isAbleToAttack;

    public int currentHealth;
    public int currentMaxHealth;
    public int currentAttack;

    public int turnCounter;

    //used in animation
    public bool isMoving;
    public bool isAttacking;


    public virtual void Awake()
    {

         GetComponent<Renderer>().material = defualtMaterial; 
         animator = GetComponent<Animator>();

         if(unit.animatorController != null){
            animator.runtimeAnimatorController = unit.animatorController;
         }

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

    void Update(){

        //check if there is an animator
        if(animator.runtimeAnimatorController == null){
            return;
        }

        //update the animator
        animator.SetBool("isMoving",isMoving);
        animator.SetBool("isAttacking",isAttacking);
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
        Board_Manager.instance.ClearBoardIndicators();
    }

    public virtual void removeUnit(){}
}
