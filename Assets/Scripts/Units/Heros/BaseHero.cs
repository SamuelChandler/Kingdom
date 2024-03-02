using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    public void Attack(BaseEnemy enemy)
    {
        if (enemy == null) return;
        enemy.Health = enemy.Health - this.attack;

        //check if destroyed
        if (enemy.Health < 0)
        {
            enemy.OccupiedTile.OccupiedUnit = null;
            Destroy(enemy.gameObject);
        }
    }
}
