using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlameTile : Tile, ITileEndTurnEffect
{

    [SerializeField] private Color _baseColor, _offsetColor;

    [SerializeField] private int DamageAmount;


    public override void Init(int a, int b)
    {
        x = a; y = b;
        _highlight.SetActive(false);
        _attackIndicator.SetActive(false);
        _moveIndicator.SetActive(false);

        _renderer.sprite = getRandomSprite();

        tileType = TileType.Flame;

    }

    public void EndTurnEffect()
    {
        if(OccupiedObject != null){
            Debug.Log(OccupiedObject.card.name + " was burned by " + name);
            OccupiedObject.TakeDamage(DamageAmount);
            
        }
    }
}
