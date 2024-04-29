using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base AI for units 
public class UnitAI : MonoBehaviour
{
    public BaseEnemy e;

    //base turn should be moving to the nearest enemy and attacking it
    public virtual void TakeTurn(){
            //find all interactable units 
            List<BaseHero> interacableUnits = Board_Manager.instance.getHerosInCircleArea(new Vector2(e.OccupiedTile.x, e.OccupiedTile.y), e.unit.speed);

            Debug.Log("Interactable Units: "+interacableUnits.Count);
    }
}

