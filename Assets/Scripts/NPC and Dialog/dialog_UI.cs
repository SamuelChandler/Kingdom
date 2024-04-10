using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dialog_UI : MonoBehaviour
{
    public static dialog_UI instance;

    [SerializeField] TextMeshProUGUI DialogText;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] GameObject Dialog;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Dialog.SetActive(false);
    }

    private void FixedUpdate()
    {
       
    }
    public void displayOneLiner(string name, string text)
    {
        NameText.text = name;
        DialogText.text = text;
        Dialog.SetActive(true);
    }

    

}


