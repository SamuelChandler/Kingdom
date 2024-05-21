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
        
     
        if(t.OccupiedStructure != null){
            t.OccupiedStructure.TakeDamage(DamageAmount);
        }else if(t.OccupiedUnit != null){
            t.OccupiedUnit.TakeDamage(DamageAmount);
        }

        Debug.Log("Target Damage effect resolved");
    }
}