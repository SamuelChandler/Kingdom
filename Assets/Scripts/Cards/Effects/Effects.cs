using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Effect: ScriptableObject{
    [SerializeField] public string EffectName;
    [SerializeField] public EffectTypes Etype;
}

[Serializable]
[CreateAssetMenu(fileName = "OnPlayEffect",menuName = "Effect/On Play Effect")]
public class OnPlayEffect: Effect{  

    public virtual void ActivateEffect(Card c){
        Debug.Log("No On Play Effect");
    }
}

[Serializable]
public class TargetedEffect: Effect{  

    [SerializeField] public Faction targetedFaction;
    [SerializeField] public int numberTargets;

    public virtual void ActivateEffect(Tile t){
        Debug.Log("No trageted Effect Effect");
    }
}

[Serializable]
[CreateAssetMenu(fileName = "EndOfTurn_Effect",menuName = "Effect/End Of Turn Effect")]
public class EndOfTurnEffect: Effect{  

    public virtual void ActivateEffect(Structure s){
        Debug.Log("No Effect");
    }
}






public enum EffectTypes{
    TargetedDamageEffect,
    EndOfTurn_Effect,
    
}