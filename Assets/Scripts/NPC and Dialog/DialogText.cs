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

    public NextDialog[] nextDialogs;

    public DialogText getNextDialogText(List<int> events){

        if(nextDialogs.Length == 0){
            return this;
        }

        foreach(NextDialog nextDialog in nextDialogs){
            if(events.Contains(nextDialog.eventId)){
                return nextDialog.targetDialogItem.getNextDialogText(events);
            }
        }

        return this;
    }
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

[Serializable]
public class NextDialog{
    public DialogText targetDialogItem;
    public int eventId;
}