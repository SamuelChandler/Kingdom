using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Board_Manager : MonoBehaviour
{
    public static Board_Manager instance;

    [SerializeField] private ScriptableMap _map;

    [SerializeField] private Transform _camera;

    private Dictionary<Vector2, Tile> _tiles;

    public BaseEnemy Enemy_Prefab;
    public BaseHero Hero_Prefab;

    private List<BaseHero> _heroes = new List<BaseHero>();
    private List<BaseEnemy> _enemies = new List<BaseEnemy>();

    private void Awake()
    {
        instance = this;
    }

    //generates grid for the game , creates tiles 
    public void generateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        var log = 0;
        var j = 0;
        for(int i = 0; i < _map.MapTiles.Length; i++){

            var id = _map.MapTiles.ElementAt(i);

            if(id != '\n')
            {
                var spawnedTile = Instantiate(_map.GetTile(id), new Vector3(log,-j), Quaternion.identity);
                spawnedTile.name = $"Tile {log} {j}";

                spawnedTile.Init(log, -j);

                _tiles[new Vector2(log, j)] = spawnedTile;

                log++;
            }
            else
            {
                j++;
                log = 0;
               
            }
        }

        _camera.transform.position = new Vector3((float)_map.width/ 2- 0.5f, -1 * ((float)_map.height /2 - 0.5f),-1);

        Game_Manager.instance.ChangeState(GameState.SpawnHero);
    }


    //returns a tile for a givem v2 positon
    public Tile GetTileAtPosition(Vector2 position)
    {
        if(_tiles.TryGetValue(position, out Tile tile))
        {
            return tile;
        }

        return null;
    }

    //returns a Unit for a given v2 position
    public BaseUnit GetUnitAtPosition(Vector2 position)
    {
        //checks if tile exists
        if (_tiles.TryGetValue(position,out Tile tile))
        {
            if (tile.OccupiedUnit != null)
            {
                return tile.OccupiedUnit;
            }
        }

        return null;
    }

    //Attempts to summon a unit to the destTile 
    public void SummonUnit(Tile destTile,BaseUnit unit)
    {
        if(unit.unit.Faction == Faction.Hero)
        {
            //do not summon a ally unit that cannot be played
            if (!Game_Manager.instance.CanBePlayed(unit)) { return; }

            //pay cost relating to Unit
            Game_Manager.instance.DecreaseCurrentInsperation(unit.unit.inspirationCost);

            //create and set unit to tile
            var summonded_Hero = (BaseHero)Instantiate(unit);
            destTile.setUnit(summonded_Hero);

            _heroes.Add(summonded_Hero);
        }
        
    }

    //Attempts to summon a unit to a dest Tile based on a scriptable unit
    public void SummonUnit(Tile destTile, ScriptableUnit unit)
    {

        if (destTile == null)
        {
            Debug.Log("Dest Tile does not exist");
            return;
        }
        if (unit == null)
        {
            Debug.Log("Sciptable Unit not Defined");
            return;
        }

        if (unit.Faction == Faction.Hero)
        {
            Hero_Prefab.unit = unit;
            //do not summon a ally unit that cannot be played
            if (!Game_Manager.instance.CanBePlayed(Hero_Prefab)) { return; }

            //pay cost relating to Unit
            Game_Manager.instance.DecreaseCurrentInsperation(Hero_Prefab.unit.inspirationCost);

            //create and set unit to tile
            var summonded_Hero = Instantiate(Hero_Prefab);
            destTile.setUnit(summonded_Hero);

            _heroes.Add(summonded_Hero);
        }

        else if (unit.Faction == Faction.Enemy)
        {
            Enemy_Prefab.unit = unit;

            //create and set unit to tile
            BaseEnemy summonded_Enemy = Instantiate(Enemy_Prefab);
            destTile.setUnit(summonded_Enemy);

            Game_Manager.instance.eAI.elist.Add(summonded_Enemy);
            _enemies.Add(summonded_Enemy);

        }
    }

    //used to spawn all the enemies for the enemy list in the map scriptable object
    public void SpawnEnemies()
    {
        Debug.Log("Spawning Enemies");
        foreach (EnemyAndPoint item in _map.enemies)
        {
            SummonUnit(GetTileAtPosition(item.loc), item.enemy);

        }
    }

    //used to move a unit from one tile to another 
    public void MoveUnit(Tile destTile, BaseUnit unit)
    {
        if(unit == null || destTile == null || !unit.isAbleToMove) return; //do nothing if the unit or tile does not exist. 

        Tile sourceTile = unit.OccupiedTile;

        //determine the change in the x and y axis
        int x_change = Mathf.Abs(sourceTile.x - destTile.x);
        int y_change = Mathf.Abs(sourceTile.y - destTile.y);

        int total_change  = x_change + y_change;


        if (total_change > unit.unit.speed)
        {
            //if the total change is greater than the units speed then do nothing and deselect
            Unit_Manager.instance.SetSelectedHero((BaseHero)null);
            return;  
        }

        destTile.setUnit(unit);
        Unit_Manager.instance.SetSelectedHero((BaseHero)null);

        //remove movment token
        unit.isAbleToMove = false;
    }

    public List<BaseHero> getHerosInCircleArea(Vector2 pos, int radius)
    {
        List<BaseHero> r = new List<BaseHero>();

        foreach (BaseHero h in _heroes)
        {
            //check if in x bounds 
            if(h.OccupiedTile.x <= pos.x+radius && h.OccupiedTile.x >= pos.x - radius)
            {
                //check if in y bounds
                if(h.OccupiedTile.y <= pos.y + radius && h.OccupiedTile.y >= pos.y - radius)
                {
                    r.Add(h);
                }
            }
        }

        return r;
    }
}
