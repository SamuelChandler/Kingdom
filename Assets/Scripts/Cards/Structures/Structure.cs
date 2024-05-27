using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//base class for all structure types 
public class Structure : MonoBehaviour
{
    [SerializeField]
    public Tile[,] OccupiedTiles;

    [SerializeField]
    public ScriptableStructure _structure;

    [SerializeField]
    public SpriteRenderer spriteRenderer;

    [SerializeField] private TextMeshProUGUI health;

    [SerializeField]
    public int currentHealth;

    public int turnCounter;


    private void Awake()
    {
        spriteRenderer.sprite = _structure.image;
        currentHealth = _structure.health;
        health.text = currentHealth.ToString();
        turnCounter = 0;
    }

    public void ActivateEndOfTurnEffects(){
        turnCounter++;
        
        _structure.EndOfTurn.ActivateEffect(this);
        
        
    }

    public void UpdateHealthDisplay(){
        health.text = currentHealth.ToString();
    }

    public void TakeDamage(int d){
        currentHealth = currentHealth-d;

        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            foreach(Tile t in OccupiedTiles){
                t.OccupiedUnit = null;
            }
            removeStructure();

            Destroy(gameObject);

        }
    }

    public virtual void removeStructure(){}


}
