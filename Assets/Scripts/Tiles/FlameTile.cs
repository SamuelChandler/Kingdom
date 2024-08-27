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

    }

    public void EndTurnEffect()
    {
        if(OccupiedUnit != null){
            Debug.Log(OccupiedUnit.unit.name + " was burned by " + name);
            OccupiedUnit.TakeDamage(DamageAmount);
        }

        if(OccupiedStructure != null){
            Debug.Log(OccupiedStructure._structure.name + " was burned");
            OccupiedStructure.TakeDamage(DamageAmount);
        }
    }
}