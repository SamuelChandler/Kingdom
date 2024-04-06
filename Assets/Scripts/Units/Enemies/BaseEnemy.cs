using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public void Attack(BaseHero hero)
    {
        if (hero == null) return;
        hero.unit.health = hero.unit.health - this.unit.attack;

        if (hero.unit.health < 0)
        {
            Destroy(hero.gameObject);
        }
    }
}
