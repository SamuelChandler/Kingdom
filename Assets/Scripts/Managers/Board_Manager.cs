using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Board_Manager : MonoBehaviour
{
    public static Board_Manager instance;

    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _grassTile,_mountainTile;

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
        for(int i = 0; i < _width; i++){
            for(int j = 0; j < _height; j++){
                var randTile = Random.Range(0,6) == 3 ? _mountainTile : _grassTile;
                var spawnedTile = Instantiate(randTile, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {j}";

                
                spawnedTile.Init(i,j);

                _tiles[new Vector2(i, j)] = spawnedTile;
            }
        }

        _camera.transform.position = new Vector3((float)_width/2- 0.5f,(float)_height/2 - 0.5f,-1);

        Game_Manager.instance.ChangeState(GameState.SpawnHero);
    }

    //spawns a random hero to the left of the screen on a walkable tile 
    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _width/2 && t.Value.Walkable).OrderBy(t=>Random.value).First().Value;
    }

    //spawns a random eneny to the right on a walkable tile 
    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
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

        //determine the change in the x and y axis
        int x_change = Mathf.Abs(sourceTile.x - destTile.x);
        int y_change = Mathf.Abs(sourceTile.y - destTile.y);

        int total_change  = x_change + y_change;


        if (total_change > unit.Speed)
        {
            //if the total change is greater than the units speed then do nothing and deselect
            Unit_Manager.instance.SetSelectedHero(null);
            return;  
        }

        destTile.setUnit(unit);
        Unit_Manager.instance.SetSelectedHero(null);


    }
}
