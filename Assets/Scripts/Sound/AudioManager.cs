using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using MankindGames;

public class AudioManager : MonoBehaviour {

    //List<AudioSource> sfxSources;
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

    //void Update()
    //{
    //    if (playerStateUI != null)
    //    {
    //        if (getSliderJustOnce == false)
    //        {

    //            playerStateUI = GameObject.Find("PlayerStateUI(Clone)");
    //            //Log.log("name " + playerStateUI.transform.Find("PauseMenu").transform.gameObject.name);
    //            sfxSlider = playerStateUI.transform.Find("PauseMenu").
    //                gameObject.transform.GetChild(0).
    //                gameObject.transform.GetChild(0).gameObject.
    //                transform.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.
    //                GetComponent<Slider>();

    //            getSliderJustOnce = true;
    //        }
    //        UpdateSFXVolume();
    //    }
    //}
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

    //public void UpdateBGMVolume(){
    //    bgmSource.volume = bgmSlider.value;
    //}

    //public void AddSFXSource(AudioSource source)
    //{
    //    sfxSources.Add(source);
    //    source.volume = sfxSlider.value;
    //}

    //public void RemoveSFXSource(AudioSource source)
    //{
    //    sfxSources.Remove(source);
    //}
}
