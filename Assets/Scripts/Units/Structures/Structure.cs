using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for all structure types 
public class Structure : MonoBehaviour
{
    [SerializeField]
    public Tile[,] OccupiedTiles;

    [SerializeField]
    public ScriptableStructure _structure;

    [SerializeField]
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    public int currentHealth;


    private void Awake()
    {
        spriteRenderer.sprite = _structure.image;
        currentHealth = _structure.health;
    }

    public void ActivateEndOfTurnEffects(){
        foreach( EndOfTurnEffects effect in _structure.effects){
            effect.ActivateEffect(this);
        }
    }


}
