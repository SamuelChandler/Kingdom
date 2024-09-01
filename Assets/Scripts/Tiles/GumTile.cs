using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GumTile : Tile
{

    [SerializeField] private Color _baseColor, _offsetColor;

    [SerializeField] private int _slowAmount;

    


    public override void Init(int a, int b)
    {
        x = a; y = b;
        _highlight.SetActive(false);
        _attackIndicator.SetActive(false);
        _moveIndicator.SetActive(false);

        _renderer.sprite = getRandomSprite();

    }

    public override void ApplyTileEffect(BoardObject boardObject)
    {
        if(boardObject is BaseUnit){
            BaseUnit unit = (BaseUnit)boardObject;
            unit.currentSpeed -= _slowAmount;
        }

        return;
    }

    public override void UnApplyTileEffect(BoardObject boardObject)
    {
        if(boardObject is BaseUnit){
            BaseUnit unit = (BaseUnit)boardObject;
            unit.currentSpeed += _slowAmount;
        }

        return;
    }
}
