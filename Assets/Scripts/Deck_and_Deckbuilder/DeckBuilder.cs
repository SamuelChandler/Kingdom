using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField]
    private CardSelectionWindow _sWindow;

    [SerializeField]
    private Set currentCardSet;

    public void Awake(){
        _sWindow.SetCards(currentCardSet);
    }




}