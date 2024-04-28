using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public string tileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected GameObject _highlight;
    [SerializeField] private bool _isWalkable;
    public int x, y;

    public BaseUnit OccupiedUnit;
    public Structure OccupiedStructure;
    public bool Walkable => (_isWalkable && OccupiedUnit == null && OccupiedStructure == null);

    public virtual void Init(int a,int b)
    {
        _highlight.SetActive(false);
        x = a; y = b;

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

    public Tile setStructure(Structure structure){
        structure.transform.position = transform.position;
        OccupiedStructure = structure;
        return this;
    }

    //sets a unit to be occupying a tile 
    public void setUnit(BaseUnit unit)
    {
        //removes unit from the tile it was on logically, if it is ocuppying a tile
        if (unit.OccupiedTile != null)
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }

        //sets unit to the selected tiles position. also sets references to each other
        unit.transform.position = transform.position; 
        OccupiedUnit = unit; 
        unit.OccupiedTile = this;
    }

    //helper to get unit. might be more complicated later
    public BaseUnit getUnit()
    {
        return OccupiedUnit;
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
            if (OccupiedUnit.unit.Faction == Faction.Hero) Unit_Manager.instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else
            {
                if (Unit_Manager.instance.SelectedHero != null)
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    Unit_Manager.instance.SelectedHero.Attack(enemy);
                    Unit_Manager.instance.SetSelectedHero((BaseHero)null);
                }
            }
        }
        else //tile is empty
        {
            //if a hero is selected set that unit into to unselected tile and unset the previosly occupied tile 
            if (Unit_Manager.instance.SelectedHero != null) 
            {

                if(this._isWalkable == false) return;

                // if unit does not have a tile it is not summoned, summon it on selected tile
                if (Unit_Manager.instance.SelectedHero.OccupiedTile == null)
                {
                    //Attempt to Summon Unit to tile
                    Board_Manager.instance.SummonUnit(this,Unit_Manager.instance.SelectedHero);
                }
                else //move the unit to the selected tile
                {
                    Board_Manager.instance.MoveUnit(this, Unit_Manager.instance.SelectedHero);
                }
               
                
            }
        }
    }

}

