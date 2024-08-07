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
    public int currentMaxHealth;

    public int turnCounter;


    private void Awake()
    {
        spriteRenderer.sprite = _structure.image;
        currentHealth = _structure.health;
        currentMaxHealth = currentHealth;
        health.text = currentHealth.ToString();
        turnCounter = 0;

        SetBuffs();

        Event_Manager.OnRefresh += Refresh;
    }

    public void Refresh(){
        turnCounter++;

        if(_structure.StartOfTurn != null){
            _structure.StartOfTurn.ActivateEffect(this);
        }
        
    }

    public void ActivateEndOfTurnEffects(){
        if(_structure.EndOfTurn != null){
            _structure.EndOfTurn.ActivateEffect(this);
        }
    }

    public void UpdateHealthDisplay(){
        health.text = currentHealth.ToString();
    }

    public void SetBuffs(){
        if(_structure.Faction == Faction.Hero){
            Board_Manager.instance.allyAttackBuff += _structure.allyAttackBoost;
        }
        else if(_structure.Faction == Faction.Enemy){
            Board_Manager.instance.enemyAttackBuff += _structure.allyAttackBoost;
        }else{
            Board_Manager.instance.allyAttackBuff += _structure.allyAttackBoost;
            Board_Manager.instance.enemyAttackBuff += _structure.allyAttackBoost;
        }
        Board_Manager.instance.ClearFieldBuffs();
        Board_Manager.instance.ApplyFieldBuffs();
    }

    public void ClearBuff(){
        if(_structure.Faction == Faction.Hero){
            Board_Manager.instance.allyAttackBuff -= _structure.allyAttackBoost;
        }
        else if(_structure.Faction == Faction.Enemy){
            Board_Manager.instance.enemyAttackBuff -= _structure.allyAttackBoost;
        }else{
            Board_Manager.instance.allyAttackBuff -= _structure.allyAttackBoost;
            Board_Manager.instance.enemyAttackBuff -= _structure.allyAttackBoost;
        }

        Board_Manager.instance.ClearFieldBuffs();
        Board_Manager.instance.ApplyFieldBuffs();
    }

    public void TakeDamage(int d){
        currentHealth = currentHealth-d;

        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {

            if(_structure.WhenDestroyed != null){
                _structure.WhenDestroyed.ActivateEffect(this);
            }

            foreach(Tile t in OccupiedTiles){
                t.OccupiedUnit = null;
            }
            removeStructure();

            Destroy(gameObject);

        }
    }

    

    public virtual void removeStructure(){}


}
