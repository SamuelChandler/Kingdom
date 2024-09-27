using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Effect: ScriptableObject{
    [SerializeField] public string EffectName;

    public virtual void ActivateEffect(Structure structure)
    {
        ActivateEffect((BoardObject)structure);
    }

    public virtual void ActivateEffect(BaseUnit unit)
    {
        ActivateEffect((BoardObject)unit);
    }

    public virtual void ActivateEffect(BoardObject obj)
    {
        throw new NotImplementedException();
    }

    public virtual void ActivateEffect(Tile t, Spell s)
    {

        if(t.OccupiedObject == null){
            throw new NotImplementedException();
        }else{
            ActivateEffect(t.OccupiedObject);
        }
        
    }

    public virtual void ActivateEffect(Spell spell)
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class TargetedEffect: Effect{  

    [SerializeField] public Faction targetedFaction;
    [SerializeField] public int numberTargets;

    public virtual void ActivateEffect(Tile t){
        Debug.Log("No targeted Effect Effect");
    }
}