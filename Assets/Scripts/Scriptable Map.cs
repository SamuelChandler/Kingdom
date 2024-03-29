using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Map")]
public class ScriptableMap : ScriptableObject
{
    [SerializeField] public int height;
    [SerializeField] public int width;

    [SerializeField] [Multiline] public String MapTiles = "";

    [SerializeField] public Tile emptyTile;
    [SerializeField] public Tile t1;
    [SerializeField] public Tile t2;
    [SerializeField] public Tile t3;
    [SerializeField] public Tile t4;
    [SerializeField] public Tile t5;
    
    public Tile GetTile(char id)
    {
        switch (id)
        {
            case '0':
                return emptyTile;
            case '1':
                return t1;
            case '2':
                return t2;
            case '3':
                return t3;
            case '4':
                return t4;
            case '5':
                return t5;
            default:
                return null;
        }
    }

}

