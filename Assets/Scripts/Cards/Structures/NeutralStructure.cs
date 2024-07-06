using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralStructure : Structure
{
    public override void removeStructure(){
        ClearBuff();
        Board_Manager.instance.removeNeutralStructure(this);
    }
}