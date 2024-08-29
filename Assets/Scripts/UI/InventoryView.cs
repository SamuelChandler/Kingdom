using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public GameObject _materialsScreen;
    public GameObject _keyItemScreen;

    public GameObject Buttons;

    public void Awake(){
        ClearScreens();
        ShowMaterials();
        Buttons.SetActive(true);
    }

    public void ShowMaterials(){
        ClearScreens();
        _materialsScreen.SetActive(true);
    }

    public void ShowKeyItems(){
        ClearScreens();
        _keyItemScreen.SetActive(true);
    }


    public void ClearScreens(){
        _materialsScreen.SetActive(false);
        _keyItemScreen.SetActive(false);
    }
}
