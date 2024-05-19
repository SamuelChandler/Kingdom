using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]

public class Effect: ScriptableObject{
    [SerializeField] string EffectName;
}


[Serializable]
[CreateAssetMenu(fileName = "OnPlayEffect",menuName = "Effect/On Play Effect")]
public class OnPlayEffect: Effect{  



    public virtual void ActivateEffect(Card c){
        Debug.Log("No On Play Effect");
    }
}

[Serializable]
[CreateAssetMenu(fileName = "TargetTileEffect",menuName = "Effect/Target Tile Effect")]
public class TargetedEffect: Effect{  

    [SerializeField] public Faction targetedFaction;
    [SerializeField] public int numberTargets;

    public virtual Tile SetTarget(){
        Debug.Log("Set Target is not implemented");
        return null;
    }

    public virtual void ActivateEffect(Card c){
        Debug.Log("No trageted Effect Effect");
    }
}




[Serializable]
[CreateAssetMenu(fileName = "EndOfTurn_Effect",menuName = "Effect/End Of Turn Effect")]
public class EndOfTurnEffect: Effect{  

    public virtual void ActivateEffect(Structure s){
        Debug.Log("No Effect");
    }
}



[Serializable]
[CreateAssetMenu(fileName = "Effect_Timed_Unit_Spawner",menuName = "New Effect/Timed Unit Spawner")]
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
