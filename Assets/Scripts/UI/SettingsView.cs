using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    public GameObject _SettingsScreen;

    [SerializeField] private Slider sfxAudio;
    [SerializeField] private Slider musicAudio;

    public void Awake(){


        sfxAudio.value = AudioManager.instance.SFX_Volume;
        musicAudio.value = AudioManager.instance.MusicVolume;
    }

}


