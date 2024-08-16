using System.Collections;
using System.Collections.Generic;
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

    }

    public void EndTurnEffect()
    {
        if(OccupiedUnit != null){
            OccupiedUnit.TakeDamage(DamageAmount);
        }

        if(OccupiedStructure != null){
            OccupiedStructure.TakeDamage(DamageAmount);
        }
    }
}
