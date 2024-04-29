using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public UnitAI uAI;

    private void Awake(){
        base.Awake();
        uAI.e = this;
    }

    public bool Attack(BaseHero hero)
    {
        if (hero == null) return false;


        //check if enemy is within one space
        if (Mathf.Abs(this.OccupiedTile.x - hero.OccupiedTile.x) > 1 || Mathf.Abs(this.OccupiedTile.y - hero.OccupiedTile.y) > 1)
        {
            Debug.Log("Attack was out of Range");
            return false;
        }

        
        hero.currentHealth = hero.currentHealth - this.unit.attack;

        if (hero.currentHealth <= 0)
        {
            Destroy(hero.gameObject);
            
        }

        return true;
    }
}
