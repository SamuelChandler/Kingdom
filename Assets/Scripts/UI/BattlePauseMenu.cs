using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattlePauseMenu : MonoBehaviour
{
    public GameObject _SettingsScreen;

    public TextMeshProUGUI Battle_Goal;

    [SerializeField] private Slider sfxAudio;
    [SerializeField] private Slider musicAudio;

    public bool isPaused;

    public void Awake(){
    
        isPaused = false;

        sfxAudio.value = AudioManager.instance.SFX_Volume;
        musicAudio.value = AudioManager.instance.MusicVolume;

        gameObject.SetActive(false);
    }

    public void SetGoalText(String s){
        Battle_Goal.text = s;
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        AudioManager.instance.Play("ButtonPress2");
        gameObject.SetActive(false);
    }

    public void Concede(){
        Hide();
        Game_Manager.instance.ChangeState(GameState.GameLoss);
    }

    public void SetMusicVol(Slider s){
        AudioManager.instance.SetMusicVol(s.value);
    }

    public void SetEffectVol(Slider s){
        AudioManager.instance.SetSfxVol(s.value);
        AudioManager.instance.Play("ButtonPress1");
    }

}