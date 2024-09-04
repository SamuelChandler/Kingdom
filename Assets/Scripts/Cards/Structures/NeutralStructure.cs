using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralStructure : Structure
{
    public override void removeStructure(){
        ClearBuff();
        Board_Manager.instance.removeNeutralStructure(this);
    }

    public override IEnumerator PlayDamagedAnimation(int d)
    {
        float dur = Game_Manager.AttackDuration;
        yield return new WaitForSeconds(dur);

        UpdateHealthDisplay();
    }
}