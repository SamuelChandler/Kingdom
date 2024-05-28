using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Draw Effect",menuName = "Effect/Draw")]
public class DrawEffect: Effect{

    public int _amountToDraw;


    public override void ActivateEffect(Structure s){
        Game_Manager.instance.Draw(_amountToDraw);
    }

    public override void ActivateEffect(BaseUnit u){
        Game_Manager.instance.Draw(_amountToDraw); 
    }
}