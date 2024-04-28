using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for all structure types 
public class Structure : MonoBehaviour
{
    public Tile[,] OccupiedTiles;

    public ScriptableStructure _structure;

    public SpriteRenderer spriteRenderer;

    public int currentHealth;


    private void Awake()
    {
        spriteRenderer.sprite = _structure.image;
        currentHealth = _structure.health;
    }

    
}
