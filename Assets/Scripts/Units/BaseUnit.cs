using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile OccupiedTile;
    public ScriptableUnit unit;
    public SpriteRenderer spriteRenderer;

    public event EventHandler Refresh;

    public bool isAbleToMove;
    public bool isAbleToAttack;

    public int currentHealth;

    private void Awake()
    {
        spriteRenderer.sprite = unit.image;
        currentHealth = unit.health;

        isAbleToAttack = false;
        isAbleToMove = false;

        Event_Manager.OnRefresh += RefreshAttackAndMove;

    }

    void RefreshAttackAndMove()
    {
        isAbleToMove=true;
        isAbleToAttack=true;
    }
}
