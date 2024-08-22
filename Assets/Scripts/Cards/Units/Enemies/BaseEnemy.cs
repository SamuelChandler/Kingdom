using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public UnitAI uAI;

    public override void Awake(){
        base.Awake();
        uAI.e = this;

        currentAttack += Board_Manager.instance.enemyAttackBuff;
        UpdateAttackAndHealthDisplay();
    }

    public void Refresh(){
        
        isAbleToMove=true;
        isAbleToAttack=true;
        turnCounter++;

        if(unit.OnStartOfTurn != null){
            unit.OnStartOfTurn.ActivateEffect(this);
        }
    }

    public bool Attack(BaseHero hero)
    {   
        //verify hero exists 
        if (hero == null) return false;

        //check if enemy is within one space
        if (Mathf.Abs(this.OccupiedTile.x - hero.OccupiedTile.x) > 1 || Mathf.Abs(this.OccupiedTile.y - hero.OccupiedTile.y) > 1)
        {
            return false;
        }

        StartCoroutine(PlayAttackAnimation());

        if(unit.OnAttack != null){
            Debug.Log("Attack Trigger");
            unit.OnAttack.ActivateEffect(this);
        }

        hero.TakeDamage(currentAttack);

        return true;
    }

    IEnumerator PlayAttackAnimation(){

        float dur = Game_Manager.AttackDuration;

        //tell the unit it is now moving 
        isAttacking = true;

        yield return new WaitForSeconds(dur);

        //tell unit that it is no longer moving
        isAttacking = false;
    }

    public override void removeUnit()
    {
        
        Board_Manager.instance.RemoveEnemy(this);

        if(unit.OnDeath != null){
            unit.OnDeath.ActivateEffect(this);
        }
        
        

    }
}
