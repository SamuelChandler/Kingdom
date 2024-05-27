using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Effect: ScriptableObject{
    [SerializeField] public string EffectName;
    [SerializeField] public EffectTypes Etype;

    public virtual void ActivateEffect(Structure structure)
    {
        throw new NotImplementedException();
    }

    public virtual void ActivateEffect(Unit unit)
    {
        throw new NotImplementedException();
    }

    public virtual void ActivateEffect(Spell spell)
    {
        throw new NotImplementedException();
    }
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

public enum EffectTypes{
    TargetedDamageEffect,
    EndOfTurn_Effect,
    
}