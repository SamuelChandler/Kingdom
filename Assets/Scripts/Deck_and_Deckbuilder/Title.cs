using System.Collections;
using System.Collections.Generic;
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
  
    }
    
}
