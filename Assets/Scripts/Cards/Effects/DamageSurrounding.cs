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
            BaseEnemy[] possibleEnemies = Board_Manager.instance._enemies.ToArray();
            foreach(BaseEnemy e in possibleEnemies){
                bool withinOne = Board_Manager.instance.WithinOne(e.OccupiedTile,s.OccupiedTile);
                if(withinOne){
                    e.TakeDamage(_damageAmount);
                }
            }

            //for each enemy structure, check distance and apply damage accourdingly
            Structure[] possibleEnemyStruts = Board_Manager.instance._EnemyStructures.ToArray();
            foreach(EnemyStructure e in possibleEnemyStruts){
                 bool withinOne = Board_Manager.instance.WithinOne(e.OccupiedTile,s.OccupiedTile);
                if(withinOne){
                    e.TakeDamage(_damageAmount);
                }
            }


        }else if(s._structure.Faction == Faction.Enemy){

            //for each enemy, check distance and apply damage accourdingly
            BaseHero[] possibleHeros = Board_Manager.instance._heroes.ToArray();
            foreach(BaseHero h in possibleHeros){
                bool withinOne = Board_Manager.instance.WithinOne(h.OccupiedTile,s.OccupiedTile);
                if(withinOne){
                    h.TakeDamage(_damageAmount);
                }
            }

            //for each enemy structure, check distance and apply damage accourdingly
            Structure[] possibleAllyStructs = Board_Manager.instance._AllyStructures.ToArray();
            foreach(AllyStructure h in possibleAllyStructs){
                bool withinOne = Board_Manager.instance.WithinOne(h.OccupiedTile,s.OccupiedTile);
                if(withinOne){
                    h.TakeDamage(_damageAmount);
                }
            }
        }
    }

    public override void ActivateEffect(BaseUnit u){
        if(u.unit.Faction == Faction.Hero){
            
            //for each enemy, check distance and apply damage accourdingly
            BaseEnemy[] possibleEnemies = Board_Manager.instance._enemies.ToArray();
            foreach(BaseEnemy e in possibleEnemies){
                bool withinOne = Board_Manager.instance.WithinOne(e.OccupiedTile,u.OccupiedTile);
                if(withinOne){
                    e.TakeDamage(_damageAmount);
                }
            }

            //for each enemy structure, check distance and apply damage accourdingly
            Structure[] possibleEnemyStruts = Board_Manager.instance._EnemyStructures.ToArray();
            foreach(EnemyStructure e in possibleEnemyStruts){
                 bool withinOne = Board_Manager.instance.WithinOne(e.OccupiedTile,u.OccupiedTile);
                if(withinOne){
                    e.TakeDamage(_damageAmount);
                }
            }


        }else if(u.unit.Faction == Faction.Enemy){

            //for each enemy, check distance and apply damage accourdingly
            BaseHero[] possibleHeros = Board_Manager.instance._heroes.ToArray();
            foreach(BaseHero h in possibleHeros){
                bool withinOne = Board_Manager.instance.WithinOne(h.OccupiedTile,u.OccupiedTile);
                if(withinOne){
                    h.TakeDamage(_damageAmount);
                }
            }

            //for each enemy structure, check distance and apply damage accourdingly
            Structure[] possibleAllyStructs = Board_Manager.instance._AllyStructures.ToArray();
            foreach(AllyStructure h in possibleAllyStructs){
                bool withinOne = Board_Manager.instance.WithinOne(h.OccupiedTile,u.OccupiedTile);
                if(withinOne){
                    h.TakeDamage(_damageAmount);
                }
            }
        }
    }
}