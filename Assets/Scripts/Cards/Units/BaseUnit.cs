using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseUnit : BoardObject
{
    
    public ScriptableUnit unit;

    //animation and art Stuff
    public SpriteRenderer spriteRenderer;

    [SerializeField] private TextMeshProUGUI attack;

    [SerializeField]  Material SelectedMaterial;

    [SerializeField] Material defualtMaterial;

    protected Animator animator;

    //unit action economy 
    public bool isAbleToMove;
    public bool isAbleToAttack;

    //unit stats
    public int currentAttack;

    public int currentSpeed;

    //unit internal logic
    public int turnCounter;

    //used in animation
    public bool isMoving;
    public bool wasMoving;
    public bool isAttacking;

    public virtual void Awake()
    {

        card = unit;
        faction = unit.Faction;

         GetComponent<Renderer>().material = defualtMaterial; 
         animator = GetComponent<Animator>();

         if(unit.animatorController != null){
            animator.runtimeAnimatorController = unit.animatorController;
         }

        spriteRenderer.sprite = unit.image;
        currentHealth = unit.health;
        currentMaxHealth = unit.health;
        currentAttack = unit.attack;
        currentSpeed = unit.speed;
        turnCounter = 0;

        if(unit._swift){
            isAbleToAttack = true;
            isAbleToMove = true;
        }else{
            isAbleToAttack = false;
            isAbleToMove = false;
        }
        

        UpdateAttackAndHealthDisplay();

    }

    void Update(){

        //check if there is an animator
        if(animator.runtimeAnimatorController == null){
            return;
        }

        if(wasMoving == true){
            //activate after movement effect 
            if(unit.afterMoving != null){
                unit.afterMoving.ActivateEffect(this);
            }

            wasMoving = false;
        }

        //update the animator
        animator.SetBool("isMoving",isMoving);
        
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
        
        UpdateHealthDisplay();
    }

    public void ReverseDamagePreview(){
        UpdateAttackAndHealthDisplay();
    }

    override public void TakeDamage(int d){
        currentHealth = currentHealth-d;

        if(unit.OnDamaged != null){
            unit.OnDamaged.ActivateEffect(this);
        }

        if (currentHealth <= 0)
        {
            OccupiedTile.OccupiedObject = null;
            removeUnit();
        }

        StartCoroutine(PlayDamagedAnimation(d));
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

    public override IEnumerator PlayDamagedAnimation(int d)
    {   
        
        float dur = Game_Manager.AttackDuration;

        float newValue = (float)currentHealth/(float)currentMaxHealth;
        
        HealthBar.value = (float)(currentHealth+d)/(float)currentMaxHealth;

        float dif = HealthBar.value - newValue;

        float TimeElapsed = 0;

        while(TimeElapsed < dur){
            
            TimeElapsed += Time.deltaTime;

            RedBar.value = (float)(currentHealth+d)/(float)currentMaxHealth;

            HealthBar.value = newValue + dif*(1-(TimeElapsed/dur));

            yield return null;
        }

        RedBar.value = HealthBar.value;

        UpdateAttackAndHealthDisplay();
    }

    public virtual void removeUnit(){}
}
