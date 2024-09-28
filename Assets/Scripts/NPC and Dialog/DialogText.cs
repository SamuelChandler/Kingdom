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

    public NextDialog[] nextDialogs;

    public DialogText getNextDialogText(List<int> events){

        if(nextDialogs == null){
            Debug.Log("No New Dialog Loaded");
            return this;
        }

        foreach(NextDialog nextDialog in nextDialogs){

            bool completed = true;

            foreach( int x in nextDialog.eventIds){
                if(!events.Contains(x)){
                    completed = false;
                }
            }

            if(completed){
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

    public bool isEnding;

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
    public int[] eventIds;
}