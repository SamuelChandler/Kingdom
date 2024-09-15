using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [NonReorderable] //Don't remove this or Element 0 will be buggy and small bc idfk
    public Sound[] sounds;

    public Sound ActiveBackgroundMusic;

    // Start is called before the first frame update

    public static AudioManager instance;

    public float MusicVolume = 0.3f;

    public float SFX_Volume = 0.5f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.blend;
            s.source.loop = s.loop;
        }

        Play("Music");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }

        if(s == ActiveBackgroundMusic){
            Debug.Log("Already Playing Song");
            return;
        }

        //stop all background music if the new sound is background music
        if(s.type == SoundType.BackgroundMusic){
            foreach(Sound sound in sounds){
                if(sound.type == SoundType.BackgroundMusic){
                    sound.source.Stop();
                }
            }
        }

        if(s.type == SoundType.BackgroundMusic){
            ActiveBackgroundMusic = s;
            s.source.volume = MusicVolume;
        }else if( s.type == SoundType.SFX){
            s.source.volume = SFX_Volume;
        }

        s.source.Play();
        Debug.Log("Playing: " + name);
    }

    public void SetMusicVol(float val){
        if(val >= 0 && val <= 1){
            MusicVolume = val;
        }
        ActiveBackgroundMusic.source.volume  = val;
    }

    public void SetSfxVol(float val){
        if(val >= 0 && val <= 1){
            SFX_Volume = val;
        }
    }
}
