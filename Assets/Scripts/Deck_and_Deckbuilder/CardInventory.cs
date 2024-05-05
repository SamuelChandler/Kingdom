using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    public List<Card> contents;

    public void AddCard(Card card){
        contents.Add(card);
    }

    public void saveInventory(){
        
    }
}
