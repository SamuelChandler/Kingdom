using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leader : BaseHero
{
    

    private void OnDestroy(){

        //check if ally leader still exists
        foreach(BaseEnemy be in Board_Manager.instance._enemies){
            if(be.unit._leader){
                //resolve game loss if leader is still alive
                Game_Manager.instance.ChangeState(GameState.GameLoss);
            }
        }
    }
}
