using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using System;

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

    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _width/2 && t.Value.Walkable).OrderBy(t=>Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if(_tiles.TryGetValue(position, out Tile tile))
        {
            return tile;
        }

        return null;
    }

    
}
