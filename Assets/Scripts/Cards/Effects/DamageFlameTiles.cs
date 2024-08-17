using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Damage Flame Tiles",menuName = "Effect/DamageFlameTiles")]
public class DamageFlameTiles: Effect{

    [SerializeField] private int DamageAmount;
    
    //damage effect trigger and determines the targets
    public override void ActivateEffect(BaseUnit u){

        //get all fire tiles
        FlameTile[] flameTiles = FindObjectsOfType(typeof(FlameTile)) as FlameTile[];

        if(u.unit.Faction == Faction.Hero){DamageEnemies(flameTiles);}
        else if (u.unit.Faction == Faction.Enemy){DamageAlly(flameTiles);}
    }

    //Checks each flame tile for enemy units and deals damage to them
    private void DamageEnemies(FlameTile[] flameTiles){
        foreach (FlameTile x in flameTiles){
            
            //check if enemy unit and deal damage if true
            if(x.OccupiedUnit == null){
                //do nothing
            }
            else if(x.OccupiedUnit.unit.Faction == Faction.Enemy){
                x.OccupiedUnit.TakeDamage(DamageAmount);
            }

            //check if enemy structure and damage if true
            if(x.OccupiedStructure == null){
                //do nothing 
            }
            else if (x.OccupiedStructure._structure.Faction == Faction.Enemy){
                x.OccupiedStructure.TakeDamage(DamageAmount);
            }
        }
    }

    //checks all Flame tiles for ally units and deals damage to them
    private void DamageAlly(FlameTile[] flameTiles){
        foreach (FlameTile x in flameTiles){
            
            //check if enemy unit and deal damage if true
            if(x.OccupiedUnit == null){
                //do nothing
            }
            else if(x.OccupiedUnit.unit.Faction == Faction.Hero){
                x.OccupiedUnit.TakeDamage(DamageAmount);
            }

            //check if enemy structure and damage if true
            if(x.OccupiedStructure == null){
                //do nothing 
            }
            else if (x.OccupiedStructure._structure.Faction == Faction.Hero){
                x.OccupiedStructure.TakeDamage(DamageAmount);
            }
        }
    }
}