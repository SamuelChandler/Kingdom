using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : BaseEnemy
{
    private void OnDestroy(){
        Game_Manager.instance.ChangeState(GameState.GameWin);
    }
}
