using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Liner_NPC : NPC
{
    [SerializeField] private string _name;
    [SerializeField] private string _oneLiner;

    public override void Interact()
    {
        dialog_UI.instance.displayOneLiner(_name, _oneLiner);
    }
}
