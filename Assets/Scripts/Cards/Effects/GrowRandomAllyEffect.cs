using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Grow Random Ally Effect",menuName = "Effect/GrowRandomAlly")]
public class GrowRandomAllyEffect: Effect{

    public int _attackGain;
    public int _healthGain;


    public override void ActivateEffect(Structure s){


        if(s._structure.Faction == Faction.Hero){
            int target = UnityEngine.Random.Range(0,Board_Manager.instance._heroes.Count);
            var unit = Board_Manager.instance._heroes[target];

            while(unit.unit._leader){
                target = UnityEngine.Random.Range(0,Board_Manager.instance._heroes.Count);
                unit = Board_Manager.instance._heroes[target];
            }

            Debug.Log("Buffing " + unit.unit.name);

            unit.currentAttack += _attackGain;
            unit.currentHealth += _healthGain;
            unit.currentMaxHealth += _healthGain;
            unit.UpdateAttackAndHealthDisplay();

            
        }else if(s._structure.Faction == Faction.Enemy){
            int target = UnityEngine.Random.Range(0,Board_Manager.instance._enemies.Count);
            var unit = Board_Manager.instance._enemies[target];

            while(unit.unit._leader){
                target = UnityEngine.Random.Range(0,Board_Manager.instance._enemies.Count);
                unit = Board_Manager.instance._enemies[target];
            }

            Debug.Log("Buffing " + unit.unit.name);

            unit.currentAttack += _attackGain;
            unit.currentHealth += _healthGain;
            unit.currentMaxHealth += _healthGain;
            unit.UpdateAttackAndHealthDisplay();
        }
        
    }

    public override void ActivateEffect(BaseUnit u){
        if(u.unit.Faction == Faction.Hero){
            int target = UnityEngine.Random.Range(0,Board_Manager.instance._heroes.Count);
            var unit = Board_Manager.instance._heroes[target];

            while(unit.unit._leader || unit == u ){
                target = UnityEngine.Random.Range(0,Board_Manager.instance._heroes.Count);
                unit = Board_Manager.instance._heroes[target];
            }
            
            Debug.Log("Buffing " + unit.unit.name);

            unit.currentAttack += _attackGain;
            unit.currentHealth += _healthGain;
            unit.currentMaxHealth += _healthGain;
            unit.UpdateAttackAndHealthDisplay();

            
        }else if(u.unit.Faction == Faction.Enemy){
            int target = UnityEngine.Random.Range(0,Board_Manager.instance._enemies.Count);
            var unit = Board_Manager.instance._enemies[target];

            while(unit.unit._leader || unit == u ){
                target = UnityEngine.Random.Range(0,Board_Manager.instance._enemies.Count);
                unit = Board_Manager.instance._enemies[target];
            }
            Debug.Log("Buffing " + unit.unit.name);

            unit.currentAttack += _attackGain;
            unit.currentHealth += _healthGain;
            unit.currentMaxHealth += _healthGain;
            unit.UpdateAttackAndHealthDisplay();
        }
    }
}