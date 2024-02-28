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

    //hover effect
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        Menu_Manager.instance.showTileInfo(this);
    }

    //removing hover effect
    private void OnMouseExit()
    { 
        _highlight.SetActive(false); 
        Menu_Manager.instance.showTileInfo(null);
    }

    //sets a unit to be occupying a tile 
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

    //events if the mouse button is pressed on a tile
    private void OnMouseDown()
    {
        //only works if it is not the heros turn
        if (Game_Manager.instance.GameState != GameState.HeroesTurn) return;

        //if tile is not empty
        if (OccupiedUnit != null)
        {
            //if a hero character set it as the selected hero. If an enemy, and a hero is selected destroy the enemy 
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
            //if a hero is selected set that unit into to unselected tile and unset the previosly occupied tile 
            if (Unit_Manager.instance.SelectedHero != null)
            {
                setUnit(Unit_Manager.instance.SelectedHero);
                Unit_Manager.instance.SetSelectedHero(null);
            }
        }
    }

}
