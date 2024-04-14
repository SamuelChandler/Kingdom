using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public void Attack(BaseHero hero)
    {
        if (hero == null) return;
        hero.currentHealth = hero.currentHealth - this.unit.attack;

        if (hero.currentHealth <= 0)
        {
            Destroy(hero.gameObject);
        }
    }
}
