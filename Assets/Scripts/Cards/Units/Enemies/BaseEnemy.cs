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
        
        isAbleToMove=1;
        isAbleToAttack=true;
        turnCounter++;

        if(unit.OnStartOfTurn != null){
            unit.OnStartOfTurn.ActivateEffect(this);
        }
    }

    public bool Attack(BoardObject allyObj)
    {   
        //verify hero exists 
        if (allyObj == null) return false;

        //check if enemy is within one space
        if (Mathf.Abs(this.OccupiedTile.x - allyObj.OccupiedTile.x) > 1 || Mathf.Abs(this.OccupiedTile.y - allyObj.OccupiedTile.y) > 1)
        {
            return false;
        }

        StartCoroutine(PlayAttackAnimation());

        if(unit.OnAttack != null){
            Debug.Log("Attack Trigger");
            unit.OnAttack.ActivateEffect(this);
        }

        allyObj.TakeDamage(currentAttack);

        return true;
    }

    IEnumerator PlayAttackAnimation(){

        float dur = Game_Manager.AttackDuration;

        //tell the unit it is now moving 
        isAttacking = true;
        animator.SetBool("isAttacking",isAttacking);

        yield return new WaitForSeconds(dur);

        //tell unit that it is no longer moving
        isAttacking = false;
        animator.SetBool("isAttacking",isAttacking);
    }

    public override void removeUnit()
    {
        
        Board_Manager.instance.RemoveEnemy(this);

        if(unit.OnDeath != null){
            unit.OnDeath.ActivateEffect(this);
        }

    }

}
