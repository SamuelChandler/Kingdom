using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base AI for units 
public class UnitAI : MonoBehaviour
{
    public BaseEnemy e;

    public void StartOfTurnEffects(){
        //activates Start of turn effects
            e.Refresh();
    }

    //base turn should be moving to the nearest enemy and attacking it
    public virtual void TakeTurn(){
            //find all interactable units 
            BoardObject ClosestAllyObj = Board_Manager.instance.getClosestAlly(new Vector2(e.OccupiedTile.x, e.OccupiedTile.y));

            //check if nearest hero exists
            if(ClosestAllyObj != null){
                //move to the closest hero and attack
                Tile destTile = Board_Manager.instance.FindNearestEmptyTile(e.OccupiedTile,ClosestAllyObj.OccupiedTile);
                Board_Manager.instance.MoveUnit(destTile,e);
                e.Attack(ClosestAllyObj);
            } 
    }
}

