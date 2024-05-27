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
    }

    public bool Attack(BaseHero hero)
    {
        if (hero == null) return false;


        //check if enemy is within one space
        if (Mathf.Abs(this.OccupiedTile.x - hero.OccupiedTile.x) > 1 || Mathf.Abs(this.OccupiedTile.y - hero.OccupiedTile.y) > 1)
        {
            return false;
        }

        hero.TakeDamage(currentAttack);

        if(unit.OnAttack != null){
            Debug.Log("Attack Trigger");
            unit.OnAttack.ActivateEffect(this);
        }

        return true;
    }

    public override void removeUnit()
    {
        Board_Manager.instance.RemoveEnemy(this);
    }
}
