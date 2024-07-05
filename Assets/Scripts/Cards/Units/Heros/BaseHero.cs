using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{

    public override void Awake(){
        base.Awake();
        Event_Manager.OnRefresh += Refresh;
    }

    public bool Attack(BaseEnemy enemy)
    {
        if (enemy == null||!this.isAbleToAttack) return false;

        //check if enemy is within one space
        if(Mathf.Abs(this.OccupiedTile.x - enemy.OccupiedTile.x) > 1|| Mathf.Abs(this.OccupiedTile.y - enemy.OccupiedTile.y) > 1)
        {
            Menu_Manager.instance.SetMessenger("Attack was out of Range");
            return false;
        }
        
        if(unit.OnAttack != null){
            Debug.Log("Attack Trigger");
            unit.OnAttack.ActivateEffect(this);
        }

        this.isAbleToAttack = false;

        enemy.TakeDamage(currentAttack);
        
        return true;
    }

    public bool Attack(EnemyStructure enemy){
        if (enemy == null||!this.isAbleToAttack) return false;

        bool inRange = false;
        
        //check if enemy is within one space
        foreach (Tile t in enemy.OccupiedTiles){
            if(Mathf.Abs(this.OccupiedTile.x - t.x) <= 1 && Mathf.Abs(this.OccupiedTile.y - t.y) <= 1)
            {
                inRange = true;
            }
        }
        if(inRange == false)
        {
            Menu_Manager.instance.SetMessenger("Attack was out of Range");
            return false;
        }

        if(unit.OnAttack != null){
            Debug.Log("Attack Trigger");
            unit.OnAttack.ActivateEffect(this);
        }

        isAbleToAttack = false;

        enemy.TakeDamage(this.currentAttack);
  
        return true;
    }

    public bool Attack(NeutralStructure enemy){
        if (enemy == null||!this.isAbleToAttack) return false;

        bool inRange = false;
        
        //check if enemy is within one space
        foreach (Tile t in enemy.OccupiedTiles){
            if(Mathf.Abs(OccupiedTile.x - t.x) <= 1 && Mathf.Abs(this.OccupiedTile.y - t.y) <= 1)
            {
                inRange = true;
            }
        }
        if(inRange == false)
        {
            Menu_Manager.instance.SetMessenger("Attack was out of Range");
            return false;
        }

        if(unit.OnAttack != null){
            Debug.Log("Attack Trigger");
            unit.OnAttack.ActivateEffect(this);
        }

        isAbleToAttack = false;

        enemy.TakeDamage(this.currentAttack);
  
        return true;
    }

    public override void removeUnit()
    {
        if(unit.OnDeath != null){
            unit.OnDeath.ActivateEffect(this);
        }

        Board_Manager.instance.RemoveHero(this);
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
