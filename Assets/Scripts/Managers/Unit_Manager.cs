/*
 * Used to Manage info with the players turn such as selected units and casting spells and abilities 
 * 
 * Features:
 * - Tracks the Selected Unit for movment puposes 
 */


using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit_Manager : MonoBehaviour
{
    public static Unit_Manager instance;

    public BaseHero SelectedHero;

    public Card SelectedCardInHand;
    bool selectingTargets;

    public BaseHero Unit_Prefab;


    private void Awake()
    {
        instance = this;
    }

    //Used to Spawn allies and handle related logic
    public void SpawnHeros()
    {
        Game_Manager.instance.ChangeState(GameState.SpawnEnemies);
    }

    //used as an in between for any logic that might occur at the time enemies are spawned
    public void SpawnEnemies()
    {
        //spawn all the enemies for a given map 
        Board_Manager.instance.SpawnMapEnemies();

        Game_Manager.instance.ChangeState(GameState.HeroesTurn);
    }
    
    //sets the selected hero based on a hero object
    public void SetSelectedHero(BaseHero Hero)
    {

        if(SelectedHero != null){
            SelectedHero.DeSelect();
        }

        SelectedHero = Hero;

        if(SelectedHero != null){
            SelectedHero.Select();
        }

        Menu_Manager.instance.showUnit(Hero);
    }

    //sets the selected hero based on a scriptable Unit
    public void SetSelectedCard(Card C)
    {
        //create unit and set to selected hero 
        SelectedCardInHand = C;

        if(SelectedHero != null){
            SelectedHero.DeSelect();
        }

        if(C.type == CardType.Unit){
            SelectedHero = Unit_Prefab;
            SelectedHero.unit = (ScriptableUnit)C;
            
        }
        
        Menu_Manager.instance.showCard(C);
    }

    public void CastSpell(Tile t){

        if(SelectedCardInHand.type != CardType.Spell){
            Debug.Log("Card Being cast is not a spell");
            return;
        }

        Spell s = (Spell)SelectedCardInHand;

        if(s.CastSpell(t)){
            SelectedCardInHand = null;
            Menu_Manager.instance.CurrentSelectedSelector.ClearCard();
        }else{
            Debug.Log("Was not able to cast the spell");
        }
    }

    

    
}
