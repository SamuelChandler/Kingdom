using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStructure : Structure
{
    public override void removeStructure(){
        ClearBuff();
        Board_Manager.instance.removeEnemyStructure(this);
    }

    public override IEnumerator PlayDamagedAnimation(int d)
    {
        float dur = Game_Manager.AttackDuration;

        float newValue = (float)currentHealth/(float)currentMaxHealth;

        float dif = HealthBar.value - newValue;

        float TimeElapsed = 0;

        while(TimeElapsed < dur){

            TimeElapsed += Time.deltaTime;

            HealthBar.value = newValue + dif*(1-(TimeElapsed/dur));

            yield return null;
        }

        RedBar.value = HealthBar.value;

        UpdateHealthDisplay();
    }
}