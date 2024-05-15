using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Liner_NPC : NPC, ITalkable
{
    [SerializeField] private DialogText _dialogText;

    private ConversationStats convoStats;

    private void Awake(){
        //set convorsation stats
        convoStats = new ConversationStats(false,true,0,_dialogText.Paragraphs.Length);
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
