using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public void Attack(BaseHero hero)
    {
        if (hero == null) return;
        hero.Health = hero.Health - this.attack;

        if (hero.Health < 0)
        {
            Destroy(hero.gameObject);
        }
    }
}
