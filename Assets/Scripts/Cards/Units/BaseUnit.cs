using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile OccupiedTile;
    public ScriptableUnit unit;
    public SpriteRenderer spriteRenderer;

    [SerializeField] private TextMeshProUGUI attack, health;

    public bool isAbleToMove;
    public bool isAbleToAttack;

    public int currentHealth;
    public int currentAttack;

    public virtual void Awake()
    {
        spriteRenderer.sprite = unit.image;
        currentHealth = unit.health;
        currentAttack = unit.attack;

        isAbleToAttack = false;
        isAbleToMove = false;

        attack.text = unit.attack.ToString();
        health.text = currentHealth.ToString();

        Event_Manager.OnRefresh += RefreshAttackAndMove;

    }

    void RefreshAttackAndMove()
    {
        isAbleToMove=true;
        isAbleToAttack=true;
    }

    public void UpdateAttackAndHealthDisplay(){
        attack.text = currentAttack.ToString();
        health.text = currentHealth.ToString();

    }

    public void TakeDamage(int d){
        currentHealth = currentHealth-d;

        if (currentHealth <= 0)
        {
            
            OccupiedTile.OccupiedUnit = null;
            
            removeUnit();

            Destroy(gameObject);

        }
    }

    public virtual void removeUnit(){}
}
