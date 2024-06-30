using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fightable_NPC : NPC, ITalkable
{
    [SerializeField] private DialogText _dialogText;

    private ConversationStats convoStats;

    private void Awake(){
        //set convorsation stats
        convoStats = new ConversationStats(false,true,0,_dialogText._dialog.Length);
        
        
    }

    new void Update()
    {
        base.Update();
    }


    public override void Interact()
    {   
        if(convoStats.ConversationPointer == 0){
            //change the dialog to the appropriate text based on game events
            List<int>eventList = new List<int>();
            eventList = DataPersistanceManager.instance._sTracker.GetEventState();
            _dialogText = _dialogText.getNextDialogText(eventList);
            convoStats = new ConversationStats(false,true,0,_dialogText._dialog.Length);
        }

        Talk();
    }

    public void LastWords(){
        Debug.Log("destroying npc");
        Destroy(gameObject);
    }

    public void Talk()
    {

        if(convoStats.Ended == true){
            convoStats = new ConversationStats(false,true,0,convoStats.DialogLength);
        }

        dialog_UI.instance.currentlyTalkingNPC = this;
        convoStats = dialog_UI.instance.DisplayNextParagraph(_dialogText,convoStats);

        //if the convorsation has ended then 
        if(convoStats.hasConversationEnded()){
            convoStats.ReadyToEnd = true;
        }else{
            //check if the current dialog point is a choice point
            if(_dialogText._dialog[convoStats.ConversationPointer].isChoice && convoStats.ReadyForNextParagraph){
                //stop reading 
                convoStats.ReadyForNextParagraph = false;
                DialogSegment temp = _dialogText._dialog[convoStats.ConversationPointer];
                dialog_UI.instance.DisplayChoices(temp._choiceOne,temp._choiceTwo);
            }
        }
        
        
        //if the dialog UI is ready move and display next paragraph
        if(convoStats.ReadyForNextParagraph){
            convoStats.ConversationPointer++;
            convoStats.ReadyForNextParagraph = false;
        }

        //if the convorsation has ended then 
        if(convoStats.hasConversationEnded()){
            convoStats.ReadyToEnd = true;
        }
    }

    // true is choice 1 and false is choice 2
    public override void ResolveChoice(bool c){
        Debug.Log("resolving choice");

        if(c){
            if(_dialogText._dialog[convoStats.ConversationPointer]._choiceOnePtr > 0){
                
                //if out of dialog bounds the conversation should end
                if(_dialogText._dialog[convoStats.ConversationPointer]._choiceOnePtr >= _dialogText._dialog.Length){
                    dialog_UI.instance.EndConversation();
                    return;
                }

                convoStats.ConversationPointer = _dialogText._dialog[convoStats.ConversationPointer]._choiceOnePtr;
                convoStats.ReadyForNextParagraph = true;
                Talk();
            }else{
                StartFight(_dialogText._dialog[convoStats.ConversationPointer].map);
            }
        }else{
            if(_dialogText._dialog[convoStats.ConversationPointer]._choiceTwoPtr > 0){
                
                //if out of dialog bounds the conversation should end
                if(_dialogText._dialog[convoStats.ConversationPointer]._choiceTwoPtr >= _dialogText._dialog.Length){
                    dialog_UI.instance.EndConversation();
                    return;
                }

                convoStats.ConversationPointer = _dialogText._dialog[convoStats.ConversationPointer]._choiceTwoPtr;
                convoStats.ReadyForNextParagraph = true;
                Talk();
            }else{
                StartFight(_dialogText._dialog[convoStats.ConversationPointer].map);
            }
        }
    }

    public void StartFight(ScriptableMap map){
        Scene_Manager.instance.GoToBattle(map);
    }

    public override void StopInteracting()
    {
        if(convoStats.ConversationPointer == 0){return;}

        dialog_UI.instance.EndConversation();
        convoStats = new ConversationStats(false,true,0,_dialogText._dialog.Length);
        
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
