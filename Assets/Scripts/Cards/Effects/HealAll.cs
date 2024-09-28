using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Heal All of Faction",menuName = "Effect/HealAll")]
public class HealAll: Effect{

    public int _healAmount;

    public Faction TargetFaction;

    public override void ActivateEffect(BaseUnit unit)
    {
        ActivateEffect((BoardObject)unit);
    }

    public override void ActivateEffect(Structure structure)
    {
        ActivateEffect((BoardObject)structure);
    }

    public override void ActivateEffect(BoardObject obj){
        
        Debug.Log(EffectName + " Activated");

        if(TargetFaction == Faction.Hero){
            foreach(BoardObject x in Board_Manager.instance.allyBoardObjects){
                x.Heal(_healAmount);
            }
        }else if(TargetFaction == Faction.Enemy){
            foreach(BoardObject x in Board_Manager.instance.enemyBoardObjects){
                x.Heal(_healAmount);
            }
        }else if(TargetFaction == Faction.Neutral){
            foreach(BoardObject x in Board_Manager.instance._NeutralStructures){
                x.Heal(_healAmount);
            }
        }

    }

    public override void ActivateEffect(Tile t, Spell s)
    {
        ActivateEffect((BoardObject)null);
    }
}