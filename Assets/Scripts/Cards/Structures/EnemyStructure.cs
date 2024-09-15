using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStructure : Structure
{
    public override void removeStructure(){
        ClearBuff();
        Board_Manager.instance.removeEnemyStructure(this);
    }

    
}