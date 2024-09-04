using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Damage Surrounding Effect",menuName = "Effect/DamageSurrounding")]
public class DamageSurrounding: Effect{

    public int _damageAmount;

    public override void ActivateEffect(BaseUnit unit)
    {
        ActivateEffect((BoardObject)unit);
    }

    public override void ActivateEffect(Structure structure)
    {
        ActivateEffect((BoardObject)structure);
    }

    public override void ActivateEffect(BoardObject obj){
        if(obj.faction == Faction.Hero){

            //for each enemy, check distance and apply damage accordingly
            BoardObject[] possibleEnemies = Board_Manager.instance.enemyBoardObjects.ToArray();
            foreach(BoardObject e in possibleEnemies){
                bool withinOne = Board_Manager.instance.WithinOne(e.OccupiedTile,obj.OccupiedTile);
                if(withinOne){
                    e.TakeDamage(_damageAmount);
                    e.PlayDamagedAnimationCoroutine(_damageAmount);
                }
            }

        }else if(obj.faction == Faction.Enemy){

            //for each enemy, check distance and apply damage accordingly
            BoardObject[] possibleHeros = Board_Manager.instance.allyBoardObjects.ToArray();
            foreach(BoardObject h in possibleHeros){
                bool withinOne = Board_Manager.instance.WithinOne(h.OccupiedTile,obj.OccupiedTile);
                if(withinOne){
                    h.TakeDamage(_damageAmount);
                    h.PlayDamagedAnimationCoroutine(_damageAmount);
                }
            }

        }
    }
}