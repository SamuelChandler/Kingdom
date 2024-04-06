using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit_Manager : MonoBehaviour
{
    public static Unit_Manager instance;

    [SerializeField]
    private List<ScriptableUnit> _units;

    public BaseHero SelectedHero;
    public BaseHero Unit_Prefab;


    private void Awake()
    {
        instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHeros()
    {
        Game_Manager.instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        

        Game_Manager.instance.ChangeState(GameState.HeroesTurn);
    }
    

    public void SetSelectedHero(BaseHero Hero)
    {
        SelectedHero = Hero;
        Menu_Manager.instance.showSelectedHero(Hero);
    }

    public void SetSelectedHero(ScriptableUnit unit)
    {
        //create unit and set to selected hero 
        SelectedHero = Unit_Prefab;
        SelectedHero.unit = unit;

        Menu_Manager.instance.showSelectedHero(unit);
    }

    
}
