using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : ScriptableObject
{

    [SerializeField] private string id;
    [ContextMenu("Generate Guid for id")]
    private void GenerateGuid(){
        id = System.Guid.NewGuid().ToString();
    }



    public Sprite image;

    public new string name;

    //Cost 
    public int inspirationCost;

    public string description; 
}