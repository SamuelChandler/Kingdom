using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile OccupiedTile;
    public ScriptableUnit unit;
    public SpriteRenderer spriteRenderer;

    public int currentHealth;

    private void Awake()
    {
        spriteRenderer.sprite = unit.image;
        currentHealth = unit.health;

    }
}
