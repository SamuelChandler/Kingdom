using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStructure : Structure
{
    public override void removeStructure(){
        Board_Manager.instance.removeAllyStructure(this);
    }
}