using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leader : BaseHero
{
    

    private void OnDestroy(){

        Game_Manager.instance.ChangeState(GameState.GameLoss);
    }
}
