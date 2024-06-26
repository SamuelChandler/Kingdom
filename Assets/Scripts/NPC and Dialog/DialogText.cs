using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogText : ScriptableObject
{
    public string Name;

    public DialogSegment[] _dialog;

    [SerializeField] public int LastWordsEvent;

    [TextArea(5,10)]
    [SerializeField] public string LastWords;

}

[Serializable]
public class DialogSegment{

    [TextArea(5,10)]
    public String Paragraph;

    //determines if there is a choice in this segment of dialog
    public bool isChoice;

    //2 choices that can be chosen
    public string _choiceOne;

    //for result 0 will do nothing and on -1 a fight will be initiated
    public int _choiceOnePtr;

    public string _choiceTwo;

    public int _choiceTwoPtr;

    public ScriptableMap map;

}