using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    

    public void StartTurn()
    {
        Debug.Log("AI has started Turn");

        Debug.Log("AI has ended Their Turn");
        
        Game_Manager.instance.ChangeState(GameState.HeroesTurn);
    }
}
