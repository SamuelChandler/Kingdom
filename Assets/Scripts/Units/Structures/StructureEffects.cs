using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Structure_Effect",menuName = "Structure/New Effect")]
public class StructureEffect:ScriptableObject{  

    public string Desc;

    public virtual void Effect(){
        Debug.Log("No Effect");
    }
}

[Serializable]
[CreateAssetMenu(fileName = "Structure_Effect_Timed_Unit_Spawner",menuName = "Structure/New Effect/Timed Unit Spawner")]
public class StructureSpawnerEffect:StructureEffect{

    public int _roundsForEachSpawn;

    public ScriptableUnit _spawn;

    public override void Effect(){
        Debug.Log("Spawning...");
    }
}
