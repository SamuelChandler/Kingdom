using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Damage Everything Effect",menuName = "Effect/DamageEverything")]
public class DamageAllLeaders: Effect{

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
        
        Debug.Log(EffectName + " Activated");

        foreach(BaseHero o in Board_Manager.instance._heroes){

            if(!o.unit._leader){
                continue;
            }

            o.TakeDamage(_damageAmount);
        }

        foreach(BaseEnemy o in Board_Manager.instance._enemies){

            if(!o.unit._leader){
                continue;
            }
            
            o.TakeDamage(_damageAmount);
        }
    }
}