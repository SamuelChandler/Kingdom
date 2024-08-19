using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "If Strongest Effect",menuName = "Effect/IfStrongest")]
public class IfStrongest: Effect{

    public Effect effectToActivate;


    public override void ActivateEffect(Structure s){

        Faction HighestAttackFaction = Faction.Hero;
        int AttackOfHighest = 0;


        foreach(BaseEnemy enemy in Board_Manager.instance._enemies){
            
            //check if base unit is stronger that current strongest
            if(enemy.currentAttack > AttackOfHighest){
                HighestAttackFaction = Faction.Enemy;
                AttackOfHighest = enemy.currentAttack;
            }

        }

        foreach(BaseHero hero in Board_Manager.instance._heroes){
            //check if base unit is stronger that current strongest
            if(hero.currentAttack > AttackOfHighest){
                HighestAttackFaction = Faction.Hero;
                AttackOfHighest = hero.currentAttack;
            }
        }

        if(HighestAttackFaction == s._structure.Faction){
            effectToActivate.ActivateEffect(s);
        }

    }

    public override void ActivateEffect(BaseUnit u){

        Faction HighestAttackFaction = Faction.Hero;
        int AttackOfHighest = 0;


        foreach(BaseEnemy enemy in Board_Manager.instance._enemies){
            
            //check if base unit is stronger that current strongest
            if(enemy.currentAttack > AttackOfHighest){
                HighestAttackFaction = Faction.Enemy;
                AttackOfHighest = enemy.currentAttack;
            }

        }

        foreach(BaseHero hero in Board_Manager.instance._heroes){
            //check if base unit is stronger that current strongest
            if(hero.currentAttack > AttackOfHighest){
                HighestAttackFaction = Faction.Hero;
                AttackOfHighest = hero.currentAttack;
            }
        }

        if(HighestAttackFaction == u.unit.Faction){

            Debug.Log("If Strongest Effect Resolving");
            effectToActivate.ActivateEffect(u);
        }
    }
}