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
        Debug.Log(_map.MapTiles.Length);
        for(int i = 0; i < _map.MapTiles.Length; i++){

            var id = _map.MapTiles.ElementAt(i);

            if(id != '\n')
            {
                var spawnedTile = Instantiate(_map.GetTile(id), new Vector3(log,-j), Quaternion.identity);
                spawnedTile.name = $"Tile {log} {j}";

                spawnedTile.Init(log, -j);

                _tiles[new Vector2(log, -j)] = spawnedTile;

                log++;
                Debug.Log("created Tile");
            }
            else
            {
                j++;
                log = 0;
                Debug.Log("new row");
            }

            
            
        }

        _camera.transform.position = new Vector3((float)_map.width/ 2- 0.5f,(float)_map.height /2 - 0.5f,-1);

        Game_Manager.instance.ChangeState(GameState.SpawnHero);
    }

    //spawns a random hero to the left of the screen on a walkable tile 
    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _map.width /2 && t.Value.Walkable).OrderBy(t=>Random.value).First().Value;
    }

    //spawns a random eneny to the right on a walkable tile 
    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _map.width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
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

    public void MoveUnit(Tile destTile, BaseUnit unit)
    {
        if(unit == null || destTile == null) return; //do nothing if the unit or tile does not exist. 

        Tile sourceTile = unit.OccupiedTile;

        //in the case the unit is being spawned
        if(sourceTile == null)
        {
            destTile.setUnit(unit);
            Unit_Manager.instance.SetSelectedHero((BaseHero)null);
        }

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


    }
}
