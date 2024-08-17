using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public void StartTurn()
    {
        //Debug.Log("AI has started Turn");
        StartOfTurn();

        //perform unit actions
        UnitActions();

        Game_Manager.instance.EndEnemyTurn();
    }

    private void StartOfTurn(){
        foreach (var e in Board_Manager.instance._enemies)
        {   
            e.uAI.StartOfTurnEffects();
        }
    }

    private void UnitActions()
    {
        foreach (var e in Board_Manager.instance._enemies)
        {   
            //Debug.Log(e.unit.name);
            e.uAI.TakeTurn();
        }
    }
}
