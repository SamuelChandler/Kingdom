using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fightable_NPC : NPC, ITalkable
{
    [SerializeField] private DialogText _dialogText;

    private Dictionary<int,choice> choicePoints;

    private ConversationStats convoStats;

    [SerializeField] private ScriptableMap[] _maps;


    private void Awake(){
        //set convorsation stats
        convoStats = new ConversationStats(false,true,0,_dialogText.Paragraphs.Length);
        choicePoints = new Dictionary<int, choice>();

        //add all choices into the choice dictionary
        foreach (choice item in _dialogText._choices)
        {
            choicePoints.Add(item._choicePos, item);
        }
    }

    public override void Interact()
    {
        Talk(_dialogText);
    }

    public void Talk(DialogText dialogText)
    {   

        if(convoStats.Ended == true){
            convoStats = new ConversationStats(false,true,0,_dialogText.Paragraphs.Length);
        }

        dialog_UI.instance.currentlyTalkingNPC = this;
        convoStats = dialog_UI.instance.DisplayNextParagraph(_dialogText,convoStats);

        Debug.Log(convoStats.ConversationPointer);

        //check if the current dialog point is a dialog point
        if(choicePoints.ContainsKey(convoStats.ConversationPointer) && convoStats.ReadyForNextParagraph){
            //stop reading 
            convoStats.ReadyForNextParagraph = false;
            choice temp = choicePoints[convoStats.ConversationPointer];
            dialog_UI.instance.DisplayChoices(temp._choiceOne,temp._choiceTwo);
        }
        
        if(convoStats.ReadyForNextParagraph){
            convoStats.ConversationPointer++;
            convoStats.ReadyForNextParagraph = false;
        }

        if(convoStats.hasConversationEnded()){
            convoStats.ReadyToEnd = true;
        }
    }

    // true is choice 1 and false is choice 2
    public override void ResolveChoice(bool c){
        Debug.Log("resolving choice");

        if(c){
            if(choicePoints[convoStats.ConversationPointer]._choiceOneResult > 0){
                convoStats.ConversationPointer = choicePoints[convoStats.ConversationPointer]._choiceOneResult;
                convoStats.ReadyForNextParagraph = true;
                Talk(_dialogText);
            }else{
                StartFight(choicePoints[convoStats.ConversationPointer]._choiceOneResult);
            }
        }else{
            if(choicePoints[convoStats.ConversationPointer]._choiceTwoResult > 0){
                convoStats.ConversationPointer = choicePoints[convoStats.ConversationPointer]._choiceTwoResult;
                convoStats.ReadyForNextParagraph = true;
                Talk(_dialogText);
            }else{
                StartFight(choicePoints[convoStats.ConversationPointer]._choiceTwoResult);
            }
        }
    }

    public void StartFight(int BattleNum){
        Scene_Manager.instance.GoToBattle();
    }

    public override void StopInteracting()
    {
        if(convoStats.ConversationPointer == 0){return;}

        dialog_UI.instance.EndConversation();
        convoStats = new ConversationStats(false,true,0,_dialogText.Paragraphs.Length);
        
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
