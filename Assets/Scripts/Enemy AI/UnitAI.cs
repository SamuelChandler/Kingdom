using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base AI for units 
public class UnitAI : MonoBehaviour
{
    public BaseEnemy e;

    //base turn should be moving to the nearest enemy and attacking it
    public virtual void TakeTurn(){
            
            //activates Start of turn effects
            e.Refresh();

            //find all interactable units 
            //List<BaseHero> interacableUnits = Board_Manager.instance.getHerosInInteractableArea(new Vector2(e.OccupiedTile.x, e.OccupiedTile.y), e.unit.speed);
            BaseHero ClosestHero = Board_Manager.instance.getClosestHero(new Vector2(e.OccupiedTile.x, e.OccupiedTile.y));

            //check if nearest hero exists
            if(ClosestHero != null){
                //move to the closest hero and attack
                Tile destTile = Board_Manager.instance.FindNearestEmptyTile(e.OccupiedTile,ClosestHero.OccupiedTile);
                Board_Manager.instance.MoveUnit(destTile,e);
                e.Attack(ClosestHero);
            } 
    }
}

