using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Map")]
public class ScriptableMap : ScriptableObject
{
    [SerializeField] public int[,] map_r0;
    

    [SerializeField]
    public Tile[] tiles;

}
