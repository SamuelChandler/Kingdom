using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "SpawnRandAtTileType",menuName = "Effect/Spawn Unit at Random Tile of Type")]
public class SpawnAtRandomTileOfType: Effect{

    [SerializeField] private TileType targetTileType;

    [SerializeField] public ScriptableUnit _spawn;

    [SerializeField] public int numberToSpawn;

    public override void ActivateEffect(BoardObject obj){
        
        //get tiles of type 
        List<Tile> tilesOfType = Board_Manager.instance.getTilesOfType(targetTileType);
        int n = numberToSpawn;

        //shuffle the tiles
        tilesOfType = tilesOfType.OrderBy( x => UnityEngine.Random.value).ToList();

        foreach(Tile t in tilesOfType){
            if(t.OccupiedObject == null){
                Board_Manager.instance.SpawnUnit(t,_spawn);
                n -= 1;
            }

            if(n <= 0){
                return;
            }
        }
    }

}