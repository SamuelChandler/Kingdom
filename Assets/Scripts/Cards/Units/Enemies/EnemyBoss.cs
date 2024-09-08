using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyBoss : BaseEnemy
{
    private void OnDestroy(){
        
        //check if ally leader still exists
        foreach(BaseHero bh in Board_Manager.instance._heroes){
            if(bh.unit._leader){
                //resolve game win if leader is still alive
                Game_Manager.instance.ChangeState(GameState.GameWin);
            }
        }

        
    }
}
