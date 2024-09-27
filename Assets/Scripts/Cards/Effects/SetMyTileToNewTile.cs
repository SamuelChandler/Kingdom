using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Swap My Tile Effect",menuName = "Effect/Tile")]
public class SetMyTileToNewTile: Effect{

    [SerializeField] private Tile tile;

    public override void ActivateEffect(BoardObject u){
        Board_Manager.instance.SwapTiles(tile,u.OccupiedTile);
    }

    
}