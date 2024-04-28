/*
 * Used to Manage the interactions between the units and other game objects
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
        SelectedHero = Hero;
        Menu_Manager.instance.showSelectedHero(Hero);
    }

    //sets the selected hero based on a scriptable Unit
    public void SetSelectedHero(ScriptableUnit unit)
    {
        //create unit and set to selected hero 
        SelectedHero = Unit_Prefab;
        SelectedHero.unit = unit;

        Menu_Manager.instance.showSelectedHero(unit);
    }

    
}
