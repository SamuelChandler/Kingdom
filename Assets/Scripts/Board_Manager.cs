using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board_Manager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tile_Prefab;

    [SerializeField] private Transform _camera;

    private Dictionary<Vector2, Tile> _tiles;

    private void Start()
    {
        generateGrid();
    }

    void generateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for(int i = 0; i < _width; i++){
            for(int j = 0; j < _height; j++){
                var spawnedTile = Instantiate(_tile_Prefab, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {j}";

                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector2(i, j)] = spawnedTile;
            }
        }

        _camera.transform.position = new Vector3((float)_width/2- 0.5f,(float)_height/2 - 0.5f,-1);
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
