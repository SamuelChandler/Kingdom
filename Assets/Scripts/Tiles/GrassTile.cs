using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{

    [SerializeField] private Color _baseColor, _offsetColor;


    public override void Init(int a, int b)
    {
        x = a; y = b;
        var isOffset = (x+y)%2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _highlight.SetActive(false);
    }
}
