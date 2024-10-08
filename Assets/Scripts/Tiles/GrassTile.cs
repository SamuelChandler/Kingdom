using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{

    [SerializeField] private Color _baseColor, _offsetColor;

    


    public override void Init(int a, int b)
    {
        x = a; y = b;
        _highlight.SetActive(false);
        _attackIndicator.SetActive(false);
        _moveIndicator.SetActive(false);

        _renderer.sprite = getRandomSprite();
        tileType = TileType.grass;

    }


}
