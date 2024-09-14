using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            List<BoardObject> boardObjects = Board_Manager.instance.allyBoardObjects;

            List<BoardObject> possibleTargets = getHerosInRange(boardObjects);
            List<BoardObject> possibleVulnerableTargets = getVulnerableHeros(possibleTargets);
            BoardObject target = null;

            //get closest Hero based on what is available
            if(possibleVulnerableTargets.Count > 0)target = getClosestHero(possibleVulnerableTargets);
            else if(possibleTargets.Count > 0)target = getClosestHero(possibleTargets);
            else target = getClosestHero(boardObjects);
            

            

            //check if nearest hero exists
            if(target != null){
                Debug.Log(e.unit.name + " targeting " + target.card.name);
                //move to the closest hero and attack
                Tile destTile = Board_Manager.instance.FindNearestEmptyTile(e.OccupiedTile,target.OccupiedTile);
                Board_Manager.instance.MoveUnit(destTile,e);
                e.Attack(target);
            }else{
                Debug.Log("No Targets for " + e.unit.name);
            } 
    }

    public List<BoardObject> getHerosInRange(List<BoardObject> objects){

        List<BoardObject> heroesInRange = new List<BoardObject>();

        int speed = e.currentSpeed;
        int attackRange = 2; //might need to be changed in the future

        foreach (BoardObject obj in objects)
        {

            //pass if the object is not of the hero faction 
            if(obj.faction != Faction.Hero) continue;

            //calc the range and the distance between the objects
            int range = speed + attackRange;
            float x_change = Mathf.Abs(e.OccupiedTile.x - obj.OccupiedTile.x);
            float y_change = Mathf.Abs(e.OccupiedTile.y  - obj.OccupiedTile.y);
            float distance = x_change + y_change;

            if(x_change == range || y_change == range)continue;
            
            if(distance <= range){
                heroesInRange.Add(obj);
            }
        }

        return heroesInRange;
    }

    public List<BoardObject> getVulnerableHeros(List<BoardObject> objects){


        List<BoardObject> vulHeroes = new List<BoardObject>();

        foreach(BoardObject obj in objects){

            //just return the leader if if is venerable can win the game
            if(obj.card.type == CardType.Leader && obj.currentHealth <= e.currentAttack){
                vulHeroes = new List<BoardObject>
                {
                    obj
                };

                return vulHeroes;
            }

            if(obj.currentHealth <= e.currentAttack){
                vulHeroes.Add(obj);
            }
            
            
        }

        return vulHeroes;
    
    }

    public BoardObject getClosestHero(List<BoardObject> objects){

        BoardObject closest = null;
        float LowestDistance = float.MaxValue;

        foreach (BoardObject obj in objects)
        {
            
            float x_change = Mathf.Abs(e.OccupiedTile.x - obj.OccupiedTile.x);
            float y_change = Mathf.Abs(e.OccupiedTile.x - obj.OccupiedTile.y);
            float distance = x_change + y_change;

            if(distance < LowestDistance){
                closest = obj;
                LowestDistance = distance;
            }
        }
        return closest;
    }
}

