using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstShopkeeper : NPC, ITalkable
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

    public override void ResolveChoice(bool c)
    {
        Debug.Log("resolving choice");

        if(c){
            if(_dialogText._dialog[convoStats.ConversationPointer]._choiceOnePtr > 0){
                
                //if out of dialog bounds the conversation should end
                if(_dialogText._dialog[convoStats.ConversationPointer]._choiceOnePtr >= _dialogText._dialog.Length){
                    dialog_UI.instance.EndConversation();
                }

                convoStats.ConversationPointer = _dialogText._dialog[convoStats.ConversationPointer]._choiceOnePtr;
                convoStats.ReadyForNextParagraph = true;
                Talk();
            }else if(_dialogText._dialog[convoStats.ConversationPointer]._choiceOnePtr == -1){
                Scene_Manager.instance.GoToStarterSelect();
            }else{
                Debug.Log("Seems like nothing is here");
            }
        }else{
            if(_dialogText._dialog[convoStats.ConversationPointer]._choiceTwoPtr > 0){
                
                //if out of dialog bounds the conversation should end
                if(_dialogText._dialog[convoStats.ConversationPointer]._choiceTwoPtr >= _dialogText._dialog.Length){
                    dialog_UI.instance.EndConversation();
                }

                convoStats.ConversationPointer = _dialogText._dialog[convoStats.ConversationPointer]._choiceTwoPtr;
                convoStats.ReadyForNextParagraph = true;
                Talk();
            }else if(_dialogText._dialog[convoStats.ConversationPointer]._choiceOnePtr == -1){
                Scene_Manager.instance.GoToStarterSelect();
            }else{
                Debug.Log("Seems like nothing is here");
            }
        }
    }

    public override void StopInteracting()
    {
         if(convoStats.ConversationPointer == 0){return;}

        dialog_UI.instance.EndConversation();
        convoStats = new ConversationStats(false,true,0,_dialogText._dialog.Length);
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
}
