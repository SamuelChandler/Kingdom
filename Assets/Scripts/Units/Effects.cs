using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Structure_Effect",menuName = "StructureNew Effect")]
public class EndOfTurnEffects: ScriptableObject{  

    public virtual void ActivateEffect(ScriptableStructure s){
        Debug.Log("No Effect");
    }
}

[Serializable]
[CreateAssetMenu(fileName = "Effect_Timed_Unit_Spawner",menuName = "New Effect/Timed Unit Spawner")]
public class SpawnerEffect: EndOfTurnEffects{

    public int _roundsForEachSpawn;

    public ScriptableUnit _spawn;

    public override void ActivateEffect(ScriptableStructure s){
        Debug.Log("Spawning:" + _spawn.name);
    }
}
