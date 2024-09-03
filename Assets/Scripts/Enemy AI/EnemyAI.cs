using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    BaseEnemy[] e_units;

    public void StartTurn()
    {   

        e_units = Board_Manager.instance.GetEnemyUnits();
        //Debug.Log("AI has started Turn");
        StartOfTurn();

        //perform unit actions
        UnitActions();

        Game_Manager.instance.EndEnemyTurn();
    }

    private void StartOfTurn(){
        foreach (var e in e_units)
        {   
            e.uAI.StartOfTurnEffects();
        }
    }

    private void UnitActions()
    {
        foreach (var e in e_units)
        {   
            //Debug.Log(e.unit.name);
            e.uAI.TakeTurn();
        }
    }
}
