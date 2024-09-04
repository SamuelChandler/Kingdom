using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

//base class for all structure types 
public class BoardObject : MonoBehaviour
{
    public Tile OccupiedTile;
    public Faction faction;

    public Card card;

    public virtual void TakeDamage(int d){}

    public virtual IEnumerator PlayDamagedAnimation(int d){

        float dur = Game_Manager.AttackDuration;
        yield return new WaitForSeconds(dur);

    }

    public void PlayDamagedAnimationCoroutine(int d){
        StartCoroutine(PlayDamagedAnimation(d));
    }
}