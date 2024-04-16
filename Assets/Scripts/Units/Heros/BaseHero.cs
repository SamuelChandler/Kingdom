using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    public bool Attack(BaseEnemy enemy)
    {
        if (enemy == null||!this.isAbleToAttack) return false;

        //check if enemy is within one space
        if(Mathf.Abs(this.OccupiedTile.x - enemy.OccupiedTile.x) > 1|| Mathf.Abs(this.OccupiedTile.y - enemy.OccupiedTile.y) > 1)
        {
            Debug.Log("Attack was out of Range");
            return false;
        }
        
        enemy.currentHealth = enemy.currentHealth - this.unit.attack;
        this.isAbleToAttack = false;

        //check if destroyed
        if (enemy.currentHealth <= 0)
        {
            enemy.OccupiedTile.OccupiedUnit = null;
            Destroy(enemy.gameObject);
        }
        
        return true;


    }

    
}
