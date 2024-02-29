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
        var heroCount = 1;

        for(int i = 0; i < heroCount; i++)
        {
            //get a random hero and spawn tile
            var randPrefab = GetRandUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randPrefab);
            var randomSpawnedTile = Board_Manager.instance.GetHeroSpawnTile();

            //spawn
            randomSpawnedTile.setUnit(spawnedHero);
        }

        Game_Manager.instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            //get a random hero and spawn tile
            var randPrefab = GetRandUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randPrefab);
            var randomSpawnedTile = Board_Manager.instance.GetEnemySpawnTile();

            //spawn
            randomSpawnedTile.setUnit(spawnedEnemy);
        }

        Game_Manager.instance.ChangeState(GameState.HeroesTurn);
    }

    //Gets a random unit based in the faction argument
    private T GetRandUnit<T>(Faction faction) where T: BaseUnit
    {
        return (T) _units.Where(u  => u.Faction == faction).OrderBy(o  => Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero Hero)
    {
        SelectedHero = Hero;
        Menu_Manager.instance.showSelectedHero(Hero);
    }

    
}
