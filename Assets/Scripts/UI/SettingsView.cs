using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SettingsView : MonoBehaviour
{
    public GameObject _videoSettingsScreen;
    public GameObject _gameplaySettingsScreen;
    public GameObject _soundSettingsScreen;

    public GameObject Buttons;

    public void Awake(){
        ClearScreens();
        ShowVideoSettings();
        Buttons.SetActive(true);
    }

    public void ShowVideoSettings(){
        ClearScreens();
        _videoSettingsScreen.SetActive(true);
    }

    public void ShowGameplaySettings(){
        ClearScreens();
        _gameplaySettingsScreen.SetActive(true);
    }

    public void ShowSoundSettings(){
        ClearScreens();
        _soundSettingsScreen.SetActive(true);
    }


    public void ClearScreens(){
        _gameplaySettingsScreen.SetActive(false);
        _videoSettingsScreen.SetActive(false);
        _soundSettingsScreen.SetActive(false);
    }
}
