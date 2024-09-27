using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Refresh Movement",menuName = "Effect/RefreshMovement")]
public class RefreshMovement: Effect{


    public override void ActivateEffect(Structure s){
        Debug.Log("Structures do not move");
    }

    public override void ActivateEffect(BaseUnit u){
        u.isAbleToMove += 1;
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