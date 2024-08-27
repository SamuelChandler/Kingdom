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
            if(x.OccupiedObject == null){
                //do nothing
            }
            else if(x.OccupiedObject.faction == Faction.Enemy){
                x.OccupiedObject.TakeDamage(DamageAmount);
            }
        }
    }

    //checks all Flame tiles for ally units and deals damage to them
    private void DamageAlly(FlameTile[] flameTiles){
        foreach (FlameTile x in flameTiles){
            
            //check if enemy unit and deal damage if true
            if(x.OccupiedObject == null){
                //do nothing
            }
            else if(x.OccupiedObject.faction == Faction.Hero){
                x.OccupiedObject.TakeDamage(DamageAmount);
            }
        }
    }
}