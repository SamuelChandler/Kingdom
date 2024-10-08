using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float blend;

    public bool loop;

    public SoundType type;

    [HideInInspector]
    public AudioSource source;
}

public enum SoundType{
    SFX,
    BackgroundMusic,
    Voice
}