using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Heal Leader Effect",menuName = "Effect/Heal_Leader")]
public class HealLeaderEffect: Effect{

    public int _amountToHeal;


    public override void ActivateEffect(Structure s){
        if(s._structure.Faction == Faction.Hero){
            foreach(BaseHero h in Board_Manager.instance._heroes){
                if(h.unit._leader){
                    h.Heal(_amountToHeal);
                }
            }
        }else if(s._structure.Faction == Faction.Enemy){
            foreach(BaseEnemy e in Board_Manager.instance._enemies){
                if(e.unit._leader){
                    e.Heal(_amountToHeal);
                }
            }
        }
        
    }

    public override void ActivateEffect(BaseUnit u){
        if(u.unit.Faction == Faction.Hero){
            foreach(BaseHero h in Board_Manager.instance._heroes){
                if(h.unit._leader){
                    h.Heal(_amountToHeal);
                }
            }
        }else if(u.unit.Faction == Faction.Enemy){
            foreach(BaseEnemy e in Board_Manager.instance._enemies){
                if(e.unit._leader){
                    e.Heal(_amountToHeal);
                }
            }
        }
    }
}