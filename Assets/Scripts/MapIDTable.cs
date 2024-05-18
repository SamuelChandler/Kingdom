using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MapIDTable",menuName = "Scriptable Map/IdTable")]
public class MapIDTable : ScriptableObject
{
    [SerializeField] ScriptableMap[] IdTable;

    public int getID(ScriptableMap c){
        for(int i = 0; i < IdTable.Length; i++){
            if(c == IdTable[i]){
                return i;
            }
        }
        return -1;
    }

    public ScriptableMap getCard(int i){
        return IdTable[i];
    }
}