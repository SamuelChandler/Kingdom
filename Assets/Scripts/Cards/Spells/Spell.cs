using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "New Spell",menuName = "Card/Spell")]
public class Spell : Card
{

    [SerializeField] public Faction TargetFaction;
    
    [SerializeField] public Effect[] effects;

    [SerializeField] public int target;

    public bool CastSpell(Tile T){

        //do nothing if the spell cannot be played
        if(!Game_Manager.instance.CanBePlayed(this,T)){
            return false;
        }

        Game_Manager.instance.DecreaseCurrentInsperation(inspirationCost);

        foreach(Effect e in effects){
            resolveEffect(e,T);
        }
        return true;
    }

    public void resolveEffect(Effect e, Tile t){
        if (t.OccupiedStructure != null){   
            e.ActivateEffect(t.OccupiedStructure);
        }else if(t.OccupiedUnit != null){
            e.ActivateEffect(t.OccupiedUnit);
        }
    }

    
}
