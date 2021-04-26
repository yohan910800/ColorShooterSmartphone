using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using MankindGames;

public class AudioManager : MonoBehaviour {

    AudioSource bgmSource;
    GameObject playerStateUI;
    public Slider sfxSlider;
    public Slider bgmSlider;

    public Sound[] sounds;
    bool getSliderJustOnce=false;
    

    void Awake() {
        foreach (Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s=Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }

    public void UpdateSFXVolume(){
        foreach (Sound s in sounds)
        {
            s.source.volume = sfxSlider.value;
        }
    }

}
