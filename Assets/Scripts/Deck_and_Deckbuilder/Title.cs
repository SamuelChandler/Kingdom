using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Title : MonoBehaviour
{
   
    [SerializeField] private GameObject _titletext, titletextbox;
    [SerializeField] bool _editing;

    private void Awake(){
        titletextbox.SetActive(false);
        _titletext.SetActive(true);
        
    }

    
    public void setTitleFromTextbox(){
        titletextbox.SetActive(false);
        _titletext.SetActive(true);

        _titletext.GetComponent<TextMeshProUGUI>().text = titletextbox.GetComponent<TMP_InputField>().text;
        DeckBuilder.instance.SetTitle(titletextbox.GetComponent<TMP_InputField>().text);
  
    }

    public void setTitleDisplay(String title){
        _titletext.GetComponent<TextMeshProUGUI>().text = title;
    }
    
}
