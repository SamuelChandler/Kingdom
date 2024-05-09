using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckView : MonoBehaviour
{
    [SerializeField]
    Title title;

    [SerializeField]
    List<DeckEntry> cardInDeckView;

    [SerializeField]
    GameObject DeckEntryPrefab;

    [SerializeField]
    GameObject EntryList;

    


    public void AddCard(Card argCard){
        //create entry
        GameObject addedEntry = Instantiate(DeckEntryPrefab, EntryList.transform);

        //get and set card entry class
        DeckEntry addedEntryDE = addedEntry.transform.gameObject.GetComponent(typeof(DeckEntry)) as DeckEntry;
        addedEntryDE.SetEntry(argCard);

        //add cart entry to list
        cardInDeckView.Add(addedEntry.transform.gameObject.GetComponent(typeof(DeckEntry)) as DeckEntry);

        
    }

    public void RemoveCard(Card argCard){
        
    }
}
