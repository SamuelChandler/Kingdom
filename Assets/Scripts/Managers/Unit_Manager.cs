using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit_Manager : MonoBehaviour
{
    public static Unit_Manager instance;

    private List<ScriptableUnit> _units;

    public BaseHero SelectedHero;

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

    
}
