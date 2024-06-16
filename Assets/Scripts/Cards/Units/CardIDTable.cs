using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UnitIdTable",menuName = "Card/IdTable")]
public class CardIDTable : ScriptableObject
{
    [SerializeField] Card[] IdTable;

    public int getID(Card c){
        for(int i = 0; i < IdTable.Length; i++){
            if(c == IdTable[i]){
                return i;
            }
        }
        return -1;
    }

    public Card getCard(int i){
        return IdTable[i];
    }

    public Card[] getAllCards(){
        return IdTable;
    }
}
