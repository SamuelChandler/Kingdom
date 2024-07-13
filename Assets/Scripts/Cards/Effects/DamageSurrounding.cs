using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Damage Surrounding Effect",menuName = "Effect/DamageSurrounding")]
public class DamageSurrounding: Effect{

    public int _damageAmount;


    public override void ActivateEffect(Structure s){
        if(s._structure.Faction == Faction.Hero){

            //for each enemy, check distance and apply damage accourdingly
            foreach(BaseEnemy e in Board_Manager.instance._enemies){
                bool withinOne = Board_Manager.instance.WithinOne(e.OccupiedTile,s.OccupiedTiles[0,0]);
                if(withinOne){
                    e.TakeDamage(_damageAmount);
                }
            }

            //for each enemy structure, check distance and apply damage accourdingly
            foreach(EnemyStructure e in Board_Manager.instance._EnemyStructures){
                 bool withinOne = Board_Manager.instance.WithinOne(e.OccupiedTiles[0,0],s.OccupiedTiles[0,0]);
                if(withinOne){
                    e.TakeDamage(_damageAmount);
                }
            }


        }else if(s._structure.Faction == Faction.Enemy){

            //for each enemy, check distance and apply damage accourdingly
            foreach(BaseHero h in Board_Manager.instance._heroes){
                bool withinOne = Board_Manager.instance.WithinOne(h.OccupiedTile,s.OccupiedTiles[0,0]);
                if(withinOne){
                    h.TakeDamage(_damageAmount);
                }
            }

            //for each enemy structure, check distance and apply damage accourdingly
            foreach(AllyStructure h in Board_Manager.instance._AllyStructures){
                bool withinOne = Board_Manager.instance.WithinOne(h.OccupiedTiles[0,0],s.OccupiedTiles[0,0]);
                if(withinOne){
                    h.TakeDamage(_damageAmount);
                }
            }
        }
    }

    public override void ActivateEffect(BaseUnit u){
        if(u.unit.Faction == Faction.Hero){

            //for each enemy, check distance and apply damage accourdingly
            foreach(BaseEnemy e in Board_Manager.instance._enemies){
                bool withinOne = Board_Manager.instance.WithinOne(e.OccupiedTile,u.OccupiedTile);
                if(withinOne){
                    e.TakeDamage(_damageAmount);
                }
            }

            //for each enemy structure, check distance and apply damage accourdingly
            foreach(EnemyStructure e in Board_Manager.instance._EnemyStructures){
                 bool withinOne = Board_Manager.instance.WithinOne(e.OccupiedTiles[0,0],u.OccupiedTile);
                if(withinOne){
                    e.TakeDamage(_damageAmount);
                }
            }


        }else if(u.unit.Faction == Faction.Enemy){

            //for each enemy, check distance and apply damage accourdingly
            foreach(BaseHero h in Board_Manager.instance._heroes){
                bool withinOne = Board_Manager.instance.WithinOne(h.OccupiedTile,u.OccupiedTile);
                if(withinOne){
                    h.TakeDamage(_damageAmount);
                }
            }

            //for each enemy structure, check distance and apply damage accourdingly
            foreach(AllyStructure h in Board_Manager.instance._AllyStructures){
                bool withinOne = Board_Manager.instance.WithinOne(h.OccupiedTiles[0,0],u.OccupiedTile);
                if(withinOne){
                    h.TakeDamage(_damageAmount);
                }
            }
        }
    }
}