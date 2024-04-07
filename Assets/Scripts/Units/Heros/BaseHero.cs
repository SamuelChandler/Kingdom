using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    public void Attack(BaseEnemy enemy)
    {
        if (enemy == null) return;
        enemy.unit.health = enemy.unit.health - this.unit.attack;

        //check if destroyed
        if (enemy.unit.health < 0)
        {
            enemy.OccupiedTile.OccupiedUnit = null;
            Destroy(enemy.gameObject);
        }
    }

    
}
