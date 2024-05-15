using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fightable_NPC : NPC, ITalkable
{
    [SerializeField] private DialogText _dialogText;

    private Dictionary<int,choice> choicePoints;



    private ConversationStats convoStats;

    [SerializeField] private ScriptableMap _map;


    private void Awake(){
        //set convorsation stats
        convoStats = new ConversationStats(false,true,0,_dialogText.Paragraphs.Length);
        choicePoints = new Dictionary<int, choice>();

        //add all choices into the choice dictionary
        foreach (choice item in _dialogText._choices)
        {
            choicePoints.Add(item._choicePos, item);
        }

        Debug.Log("I am Alive");
    }

    public override void Interact()
    {
        Talk(_dialogText);
    }

    public void Talk(DialogText dialogText)
    {   
        //check if the current dialog point is a dialog point
        if(choicePoints.ContainsKey(convoStats.ConversationPointer)){
            Debug.Log("Choice point");
        }

        
        if(convoStats.Ended == true){
            convoStats = new ConversationStats(false,true,0,_dialogText.Paragraphs.Length);
        }

        convoStats = dialog_UI.instance.DisplayNextParagraph(_dialogText,convoStats);

        if(convoStats.ReadyForNextParagraph){
            convoStats.ConversationPointer++;
            convoStats.ReadyForNextParagraph = false;
        }

        if(convoStats.hasConversationEnded()){
            convoStats.ReadyToEnd = true;
        }


        
    }
}

public class ConversationStats{
    public bool ReadyToEnd;
    public int ConversationPointer;
    public bool ReadyForNextParagraph;
    public int DialogLength;

    public bool Ended;

    public ConversationStats(bool a, bool b, int c, int d){
        ReadyToEnd = a;
        ReadyForNextParagraph = b;
        ConversationPointer = c;
        DialogLength = d;
        Ended = false;
    }

    public bool hasConversationEnded(){
        if(ConversationPointer >= DialogLength){
            return true;
        }
        return false;
    }
}
