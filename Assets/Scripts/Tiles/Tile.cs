using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected GameObject _highlight;
    [SerializeField] private bool _isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init(int x,int y)
    {
        _highlight.SetActive(false);
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    { 
        _highlight.SetActive(false); 
    }

    public void setUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }

        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

}
