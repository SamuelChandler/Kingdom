using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "New Spell",menuName = "Card/Spell")]
public class Spell : Card
{

    [SerializeField] public Faction TargetFaction;
    
    [SerializeField] public Effect[] effects;

    [SerializeField] public int target;

    [SerializeField] public CardType[] targetTypes;

    public bool CastSpell(Tile T){

        //do nothing if the spell cannot be played
        if(!Game_Manager.instance.CanBePlayed(this,T)){
            return false;
        }

        if(T.OccupiedObject.faction != TargetFaction){
            return false;
        }

        if((!targetTypes.Contains(T.OccupiedObject.card.type) && !(targetTypes.Length == 0))){
            return false;
        }

        Game_Manager.instance.DecreaseCurrentInsperation(inspirationCost);

        foreach(Effect e in effects){
            e.ActivateEffect(T.OccupiedObject);
        }
        return true;
    }

    
}
