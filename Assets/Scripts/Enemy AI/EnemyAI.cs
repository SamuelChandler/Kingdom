using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public void StartTurn()
    {
        Debug.Log("AI has started Turn");

        //perform unit actions
        UnitActions();

        Game_Manager.instance.EndEnemyTurn();
    }

    private void UnitActions()
    {
        foreach (var e in Board_Manager.instance._enemies)
        {
            e.uAI.TakeTurn();
        }
    }
}
