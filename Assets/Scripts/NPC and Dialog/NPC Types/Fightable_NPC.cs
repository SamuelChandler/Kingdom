using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fightable_NPC : NPC, ITalkable
{
    [SerializeField] private DialogText _dialogText;

    

    public override void Interact()
    {
        Talk(_dialogText);
    }

    public void Talk(DialogText dialogText)
    {
        //one Liners should only have one line of text so only the first is displayed
        dialog_UI.instance.DisplayNextParagraph(dialogText);
    }
}
