using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogText : ScriptableObject
{
    public string Name;

    [TextArea(5,10)]
    public string[] Paragraphs;

    public choice[] _choices;

    [SerializeField] public ScriptableMap[] _maps;

}

[Serializable]
public class choice{

    //the location of the choice in dialog
    public int _choicePos;

    //2 choices that can be chosen
    public string _choiceOne;

    //for result 0 will do nothing and on -1 a fight will be initiated
    public int _choiceOneResult;

    public string _choiceTwo;

    public int _choiceTwoResult;

}