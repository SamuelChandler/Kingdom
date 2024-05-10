using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
   public List<Deck> _decks;

   public string PlayerName;


    public PlayerData(string name){
        PlayerName = name;
    }

}
