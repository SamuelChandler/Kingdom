using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    public void Attack(BaseEnemy enemy)
    {
        if (enemy == null||!this.isAbleToAttack) return;

        this.isAbleToAttack = false;
        enemy.currentHealth = enemy.currentHealth - this.unit.attack;

        //check if destroyed
        if (enemy.currentHealth <= 0)
        {
            enemy.OccupiedTile.OccupiedUnit = null;
            Destroy(enemy.gameObject);
        }


    }

    
}
