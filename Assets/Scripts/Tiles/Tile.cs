using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public string tileName;
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
        Menu_Manager.instance.showTileInfo(this);
    }

    private void OnMouseExit()
    { 
        _highlight.SetActive(false); 
        Menu_Manager.instance.showTileInfo(null);
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

    private void OnMouseDown()
    {
        if (Game_Manager.instance.GameState != GameState.HeroesTurn) return;

        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.Faction == Faction.Hero) Unit_Manager.instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else
            {
                if (Unit_Manager.instance.SelectedHero != null)
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    //attack 
                    Destroy(enemy.gameObject);
                    Unit_Manager.instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            if (Unit_Manager.instance.SelectedHero != null)
            {
                setUnit(Unit_Manager.instance.SelectedHero);
                Unit_Manager.instance.SetSelectedHero(null);
            }
        }
    }

}
