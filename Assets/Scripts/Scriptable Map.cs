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
    
    [SerializeField] public Tile t1;
    [SerializeField] public Tile t2;
    [SerializeField] public Tile t3;
    [SerializeField] public Tile t4;
    [SerializeField] public Tile t5;
    
}

