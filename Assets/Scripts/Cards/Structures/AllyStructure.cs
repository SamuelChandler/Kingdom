using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStructure : Structure
{
    public override void removeStructure(){
        ClearBuff();
        Board_Manager.instance.removeAllyStructure(this);
    }

    public override IEnumerator PlayDamagedAnimation(int d)
    {
        float dur = Game_Manager.AttackDuration;
        yield return new WaitForSeconds(dur);

        UpdateHealthDisplay();
    }
}