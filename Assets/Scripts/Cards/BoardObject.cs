using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

//base class for all structure types 
public class BoardObject : MonoBehaviour
{
    public Tile OccupiedTile;
    public Faction faction;

    public Card card;

    [SerializeField] public Slider HealthBar;

    [SerializeField] public Slider RedBar;

    [SerializeField]
    public int currentHealth;
    public int currentMaxHealth;

    public virtual void TakeDamage(int d){}

    public virtual IEnumerator PlayDamagedAnimation(int d){

        float dur = Game_Manager.AttackDuration;

        float newValue = (float)currentHealth/(float)currentMaxHealth;

        float dif = HealthBar.value - newValue;

        float TimeElapsed = 0;

        while(TimeElapsed < dur){

            TimeElapsed += Time.deltaTime;

            HealthBar.value = newValue + dif*(1-(TimeElapsed/dur));

            yield return null;
        }

        RedBar.value = HealthBar.value;

        
    }

    public void PlayDamagedAnimationCoroutine(int d){
        StartCoroutine(PlayDamagedAnimation(d));
    }

    public void UpdateHealthDisplay(){

        HealthBar.value = (float)currentHealth/(float)currentMaxHealth;
        RedBar.value = (float)currentHealth/(float)currentMaxHealth;

        if(currentHealth <=0 )Destroy(gameObject);
    }

    public void ShowDamagePreview(int d){
        int ID_Health = currentHealth - d;

        HealthBar.value = (float)ID_Health/(float)currentMaxHealth;
    }

}