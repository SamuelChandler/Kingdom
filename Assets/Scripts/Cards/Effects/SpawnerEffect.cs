using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Structure_Unit_Spawner",menuName = "Effect/Structure Unit Spawner")]
public class SpawnerEffect: EndOfTurnEffect{

    public int _roundsForEachSpawn;

    public ScriptableUnit _spawn;

    public override void ActivateEffect(Structure s){
        Tile dest = Board_Manager.instance.GetRandAdjactentFreeTile(s.OccupiedTiles[0,0]);
        
        if(dest == null){
            Debug.Log("No Available Tiles");
        }else if(s.turnCounter%_roundsForEachSpawn == 0){
            Board_Manager.instance.SummonUnit(dest,_spawn);
        }
        

        
    }
}