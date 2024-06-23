using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckView : MonoBehaviour
{
    [SerializeField]
    Title title;

    [SerializeField]
    LeaderEntry leaderEntry;

    [SerializeField]
    List<DeckEntry> cardInDeckView;

    [SerializeField]
    GameObject DeckEntryPrefab;

    [SerializeField]
    GameObject EntryList;

    [SerializeField]
    TextMeshProUGUI DeckLimitText;

    public void CreateAndAddCard(Card argCard){


        //if card is already in a list then just add an adittional
        if(increaseCount(argCard)){
            return;
        }

        //create entry
        GameObject addedEntry = Instantiate(DeckEntryPrefab, EntryList.transform);

        //get and set card entry class
        DeckEntry addedEntryDE = addedEntry.transform.gameObject.GetComponent(typeof(DeckEntry)) as DeckEntry;
        addedEntryDE.SetEntry(argCard);

        //add cart entry to list
        cardInDeckView.Add(addedEntry.transform.gameObject.GetComponent(typeof(DeckEntry)) as DeckEntry);
        
    }

    public bool increaseCount(Card argCard){
        foreach(DeckEntry x in cardInDeckView){
            if(x.card == argCard){
                x.AddCard();
                return true;
            }
        }
        return false;
    }

    public void updateDeckLimitText(int a, int b){
        DeckLimitText.text = a.ToString() + "/" +b.ToString();
    }

    public void RemoveEntry(DeckEntry e){
        cardInDeckView.Remove(e);
    }

    public void AddLeaderEntry(Card argCard){
        
        //set the leader card to the leader entry
        leaderEntry.SetEntry(argCard);

    }

    public void SetTitleDisplay(String titleText){
        title.setTitleDisplay(titleText);
    }
}
