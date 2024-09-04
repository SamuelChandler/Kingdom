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
            ActivateEffect(t.OccupiedObject);
        }
    }

    public override void ActivateEffect(BoardObject obj)
    {

        if(obj == null){return;}

        obj.TakeDamage(DamageAmount);
        obj.PlayDamagedAnimationCoroutine(DamageAmount);
    }
}