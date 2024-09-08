using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Swap_Surrounding_Tiles_Effect",menuName = "Effect/Swap Surrounding Tiles")]
public class SetSurroundingTiles: Effect{

    [SerializeField] private Tile tile;

    public override void ActivateEffect(BoardObject obj)
    {
        Tile[] surroundingTiles = Board_Manager.instance.GetSurroundingTiles(obj.OccupiedTile);
    
        foreach(Tile t in surroundingTiles){
            Board_Manager.instance.SwapTiles(tile,t);
        }
    }
}