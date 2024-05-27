using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Structure_Unit_Spawner",menuName = "Effect/Structure Unit Spawner")]
public class SpawnerEffect: Effect{

    public int _roundsForEachSpawn;

    public ScriptableUnit _spawn;

    public override void ActivateEffect(Structure s){
        Tile dest = Board_Manager.instance.GetRandAdjactentFreeTile(s.OccupiedTiles[0,0]);
        
        if(dest == null){
            Debug.Log("No Available Tiles for the spawn");
        }else if(s.turnCounter%_roundsForEachSpawn == 0){
            Board_Manager.instance.SpawnUnit(dest,_spawn);
        }
        
    }

    public override void ActivateEffect(BaseUnit u){
        Tile dest = Board_Manager.instance.GetRandAdjactentFreeTile(u.OccupiedTile);
        
        if(dest == null){
            Debug.Log("No Available Tiles for the spawn");
        }else if(u.turnCounter%_roundsForEachSpawn == 0){
            Board_Manager.instance.SpawnUnit(dest,_spawn);
        }
        
    }
}