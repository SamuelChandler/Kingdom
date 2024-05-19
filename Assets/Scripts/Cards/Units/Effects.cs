using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class Effect: ScriptableObject{
    
}


[Serializable]
[CreateAssetMenu(fileName = "EndOfTurn_Effect",menuName = "Effect/End Of Turn Effect")]
public class EndOfTurnEffects: Effect{  

    public virtual void ActivateEffect(Structure s){
        Debug.Log("No Effect");
    }
}



[Serializable]
[CreateAssetMenu(fileName = "Effect_Timed_Unit_Spawner",menuName = "New Effect/Timed Unit Spawner")]
public class SpawnerEffect: EndOfTurnEffects{

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
