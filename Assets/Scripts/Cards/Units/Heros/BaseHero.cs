using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{

    public override void Awake(){
        base.Awake();
        Event_Manager.OnRefresh += Refresh;

        if(unit.OnAllyDeath != null){
            Event_Manager.onAllyDeath += unit.OnAllyDeath.ActivateEffect;
        }
        
        currentAttack += Board_Manager.instance.allyAttackBuff;
        UpdateAttackAndHealthDisplay();
    }

    public bool Attack(BoardObject enemy)
    {
        if (enemy == null||!this.isAbleToAttack) return false;

        if (enemy.faction == Faction.Hero) return false;

        //check if enemy is within one space
        if(Mathf.Abs(this.OccupiedTile.x - enemy.OccupiedTile.x) > 1|| Mathf.Abs(this.OccupiedTile.y - enemy.OccupiedTile.y) > 1)
        {
            Menu_Manager.instance.SetMessenger("Attack was out of Range");
            return false;
        }

        StartCoroutine(PlayAttackAnimation());
        
        if(unit.OnAttack != null){
            Debug.Log("Attack Trigger");
            unit.OnAttack.ActivateEffect(this);
        }

        this.isAbleToAttack = false;

        enemy.TakeDamage(currentAttack);
        
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
        Board_Manager.instance.RemoveHero(this);
        
        if(unit.OnDeath != null){
            unit.OnDeath.ActivateEffect(this);
        }

        Event_Manager.instance.AllyDeath(this);

        
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

    
}
