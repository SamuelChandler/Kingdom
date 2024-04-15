using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public List<BaseEnemy> elist;

    private void Awake()
    {
        elist = new List<BaseEnemy>();
    }

    public void StartTurn()
    {
        Debug.Log("AI has started Turn");

        //perform unit actions
        UnitActions();


        Debug.Log("AI has ended Their Turn");
        Game_Manager.instance.ChangeState(GameState.HeroesTurn);
    }

    private void UnitActions()
    {
        foreach (var e in elist)
        {
            Debug.Log(e.unit.name);
        }
    }
}
