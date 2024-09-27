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
    public override void ActivateEffect(BoardObject u){
        Debug.Log("Resolving Effect "+EffectName);

        //get all fire tiles
        FlameTile[] flameTiles = FindObjectsOfType(typeof(FlameTile)) as FlameTile[];

        if(u.faction == Faction.Hero){DamageEnemies(flameTiles);}
        else if (u.faction == Faction.Enemy){DamageAlly(flameTiles);}
    }

    public override void ActivateEffect(Tile t, Spell s){

        Debug.Log("Resolving Effect "+EffectName+" Tile/Spell Mode");

        //get all fire tiles
        FlameTile[] flameTiles = FindObjectsOfType(typeof(FlameTile)) as FlameTile[];

        if(s.TargetFaction == Faction.Hero){DamageAlly(flameTiles);}
        else if (s.TargetFaction == Faction.Enemy){DamageEnemies(flameTiles);}
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

        Debug.Log("Damaging Flame Tiles: " + flameTiles.Length);
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