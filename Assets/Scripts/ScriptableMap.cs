using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Map")]
public class ScriptableMap : ScriptableObject
{
    [SerializeField] public int height;
    [SerializeField] public int width;

    [SerializeField] [TextArea(5, 10)] public String MapTiles = "";

    [SerializeField] public Tile emptyTile;
    [SerializeField] public Tile t1;
    [SerializeField] public Tile t2;
    [SerializeField] public Tile t3;
    [SerializeField] public Tile t4;
    [SerializeField] public Tile t5;

    [SerializeField] public EnemyAndPoint[] enemies;

    [Tooltip("The goal to win on this map")]
    [SerializeField] public LevelType _levelType;

    [Tooltip("Unit that will be end the game if destroyed by the player. Only Applicable when Level is set to Defeat Boss")]
    [SerializeField] public EnemyAndPoint _boss;

    [Tooltip("Number of rounds needed to win in survival mode")]
    [SerializeField] public int _survivalTurns;




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

[Serializable]
public class EnemyAndPoint
{
    public ScriptableUnit enemy;
    public Vector2 loc;
}

public enum LevelType
{
    ClearEnemies,
    DefeatBoss,
    Survive
}
