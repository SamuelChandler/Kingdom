using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Grow Effect",menuName = "Effect/Growth")]
public class GrowEffect: Effect{

    public int _attackGain;
    public int _healthGain;

    public override void ActivateEffect(Structure s){
        s.currentHealth += _healthGain;
        s.currentMaxHealth += _healthGain;
        s.UpdateHealthDisplay();
    }

    public override void ActivateEffect(BaseUnit u){
        u.currentAttack += _attackGain;
        u.currentHealth += _healthGain;
        u.currentMaxHealth += _healthGain;
        u.UpdateAttackAndHealthDisplay();   
    }

    public override void ActivateEffect(BoardObject obj)
    {
        if(obj.card.type == CardType.Structure){
            ActivateEffect((Structure)obj);
        }else if(obj.card.type == CardType.Unit || obj.card.type == CardType.Leader){
            ActivateEffect((BaseUnit)obj);
        }

        return;
    }
}