using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "TargetDamageEffect",menuName = "Effect/Target Damamge Effect")]
public class TargetedDamageEffect: TargetedEffect{  

    [SerializeField] public int DamageAmount;


    public override void ActivateEffect(Tile t){
        
     
        if(t.OccupiedObject != null){
            t.OccupiedObject.TakeDamage(DamageAmount);
        }
    }

    public override void ActivateEffect(BaseUnit unit)
    {
        unit.TakeDamage(DamageAmount);
    }

    public override void ActivateEffect(Structure s)
    {
        s.TakeDamage(DamageAmount);
    }

    public override void ActivateEffect(BoardObject obj)
    {
        obj.TakeDamage(DamageAmount);
    }
}